var nhhui = {};
nhhui.alertbox = {
    show: function (id, result) {
        var alertbox = $("#" + id);
        if (alertbox.length == 0)
            return;
        if (result == undefined)
            return;

        if (result.Code == -1) {
            alertbox.attr("class", "alert alert-danger");
        }
        else {
            alertbox.attr("class", "alert alert-success");
        }
        alertbox.html(result.Message).show();
    },
    alert: function (id, message) {
        var alertbox = $("#" + id);
        if (alertbox.length == 0)
            return;
        alertbox.html(message).attr("class", "alert alert-danger").show();
    },
};


nhhui.floorSelect = function (options) {
    this.settings = {
        controlId: "",
        url: "",
        projectId: "0",
        buildingId: "",
        floorId: "",
    };
    for (var i in options) {
        if (options.hasOwnProperty(i)) {
            this.settings[i] = options[i];
        }
    }
};
nhhui.floorSelect.prototype = {
    init: function () {
        var _this = this;
        var control = $("#" + _this.settings.controlId);
        control.change(function () {
            var selectedItem = $(this).find("option:selected");
            _this.settings.buildingId = selectedItem.attr("data-bId");
            _this.settings.floorId = selectedItem.val();
        });
        _this.bindSelect(_this.settings.projectId);
        
        
    },
    bindSelect: function (projectId) {
        _this = this;
        $.ajax({
            type: "GET",
            url: _this.settings.url,
            data: "projectId=" + projectId,
            dataType: "JSON",
            success: function (result, status) {
                var strHtml = "<option value='' data-bId=''>请选择楼层</option>";
                $(result).each(function (index, item) {
                    strHtml += "<optgroup label='" + item.Name + "'>";
                    strHtml += "<option value='' data-bId='" + item.Id + "'>" + item.Name + "</option>";
                    $(item.FloorList).each(function (index, floor) {
                        strHtml += "<option value='" + floor.FloorId + "' data-bId='" + item.Id + "'>" + item.Name + floor.FloorName + "</option>";
                    });
                    strHtml += "</optgroup>";
                });
                var control = $("#" + _this.settings.controlId);
                control.html(strHtml).val();

                if (_this.settings.floorId.length > 0) {
                    control.val(_this.settings.floorId);
                } else {
                    var _options = control.find("option[data-bId='" + _this.settings.buildingId + "']");
                    if (_options.length > 0) {
                        _options.eq(0).attr("selected", true);
                    }
                }
            },
        });
    },
    getBuildingId: function () {
        return this.settings.buildingId;
    },
    getFloorId: function () {
        return this.settings.floorId;
    },
};

nhhui.nhhtable = function (options) {
    this.setting = {
        sortBy: '',
        sortMode: '',
        url: '',
        searchBtn: '',
        onInit: undefined,
    };
    for (var i in options) {
        if (options.hasOwnProperty(i)) {
            this.setting[i] = options[i];
        }
    }
};
nhhui.nhhtable.prototype = {
    init: function () {
        var self = this;
        if (self.setting.sortBy.length > 0) {
            $(".sortLink[sort-field='" + self.setting.sortBy + "']").addClass("sort-" + self.setting.sortMode);
        }
        $(".sortLink").click(function () {
            var _data = undefined;
            if (self.setting.onInit != undefined) {
                _data = self.setting.onInit();
            }
            if (_data == null || _data == undefined) {
                _data = {};
            }
            var sortMode = self.setting.sortMode;
            if (sortMode == "asc") {
                sortMode = "desc";
            }
            else {
                sortMode = "asc";
            }
            _data.orderMode = sortMode;
            _data.orderBy = $(this).attr("sort-field");
            $(this).attr("href", self.setting.url + "?" + jQuery.param(_data))
            return true;
        });
        $(".page").click(function () {
            var _data = undefined;
            if (self.setting.onInit != undefined) {
                _data = self.setting.onInit();
            }
            if (_data == null || _data == undefined) {
                _data = {};
            }
            _data.orderBy = self.setting.sortBy;
            _data.orderMode = self.setting.sortMode;
            _data.page = $(this).attr("data-page");
            location.href = self.setting.url + "?" + jQuery.param(_data);
            return true;
        });
        if (self.setting.searchBtn.length > 0) {
            $("#" + self.setting.searchBtn).click(function () {
                var _data = undefined;
                if (self.setting.onInit != undefined) {
                    _data = self.setting.onInit();
                }
                location.href = self.setting.url + "?" + jQuery.param(_data);
            });
        }
    }
};

nhhui.nhhtable2 = function (options) {
    this.setting = {
        id: "",
        reportCode: undefined,
        searchBtn: "",
        exportBtn: "",
        cssClass: "table table-striped table-hover",
        sortName: undefined,
        sortOrder: "asc",
        page: 1,
        url: undefined,
        exportUrl: undefined,
        type: "GET",
        queryParams: function (params) {
            return params;
        },
    };
    for (var i in options) {
        if (options.hasOwnProperty(i)) {
            this.setting[i] = options[i];
        }
    }
    this.columns = undefined;
    this.data = [];
    this.pagingInfo = undefined;
    this.container = $("#" + this.setting.id);
    this.init();
};

nhhui.nhhtable2.prototype.init = function () {
    this.preInit();
    this.initToolbar();
    this.initSearch();
    this.initExport();
};

nhhui.nhhtable2.prototype.preInit = function () {
    var _self = this;
    _self.setting.reportCode = _self.container.attr("data-reportCode");
    //请求列
    var requestData = {};
    requestData.reportCode = _self.setting.reportCode;

    $.ajax({
        url: "/ajax/reportconfig/GetFieldList",
        data: requestData,
        type: "GET",
        dataType: "JSON",
        success: function (result, status) {
            _self.columns = [];
            $.each(result.FieldList, function (index, field) {
                _self.columns.push(field);
            });
            _self.bindHeader();
            _self.binbBody();
        }
    });
};

nhhui.nhhtable2.prototype.initToolbar = function () {
    var _self = this;

    var toolbar = _self.container.find(".nhhtable-toolbar");
    var title = _self.container.attr("data-title");
    var html = [];
    html.push("<span class=\"nhhtable-title\">" + title + "</span>");
    html.push("<div class=\"pull-right\">");
    html.push("<div class=\"btn-group\">");
    html.push("<button type=\"button\" class=\"btn btn-field-toggle btn-font btn-tool dropdown-toggle\" title=\"字段列表\">");
    html.push("<i class=\"columns\"></i><span class=\"caret\"></span></button>");
    html.push("<ul class=\"nhhtable-fields hide\" role=\"menu\"></ul></div></div>");

    toolbar.append(html.join(''));

    var btnFieldToggle = _self.container.find(".btn-field-toggle");
    var ulFields = _self.container.find(".nhhtable-fields");

    btnFieldToggle.click(function () {
        var html = [];
        $.each(_self.columns, function (index, field) {
            html.push("<li><label><input type='checkbox' value='" + field.FieldID + "'");
            if (field.Display) {
                html.push(" checked='checked'");
            }
            html.push(" />" + field.FieldTitle + "</label></li>");
        });
        html.push("<li style='border-bottom: 1px solid #ccc;margin-bottom: 5px;'></li>");
        html.push("<li><button type='button' class='btn btn-info btn-sm btn-field' style='margin-left: 13px;'>保存</button>");
        html.push("<button type='button' class='btn btn-default btn-sm btn-field-close' style='margin-left: 15px;'>取消</button>");
        html.push("</li>");
        ulFields.html(html.join(''));
        ulFields.removeClass("hide");

        var btnFieldClose = _self.container.find(".btn-field-close");
        $(btnFieldClose).click(function () {
            ulFields.addClass("hide");
        });

        var btnField = _self.container.find(".btn-field");
        btnField.click(function () {
            //保存
            var queryData = {};
            queryData.reportCode = _self.setting.reportCode;
            queryData.fieldList = "";
            var checkboxList = $(".nhhtable-fields").find("input[type='checkbox']");
            var fieldArr = [];
            $.each(checkboxList, function (index, item) {
                var checkbox = $(item);
                if (checkbox.prop("checked") == true) {
                    fieldArr.push(checkbox.val());
                }
            });
            if (fieldArr.length < 2) {
                nhhui.alert("至少选择两列");
                return;
            }
            queryData.fieldList = fieldArr.join(',');

            $.ajax({
                url: "/ajax/reportconfig/save",
                data: queryData,
                type: "GET",
                dataType: "JSON",
                success: function (result, status) {
                    ulFields.addClass("hide");
                    _self.preInit();
                }
            });
        });
    });
};

nhhui.nhhtable2.prototype.bindHeader = function () {
    var _self = this;

    var table = _self.container.find("table");
    table.addClass(_self.setting.cssClass);
    var thead = table.find("thead");
    if (thead == undefined || thead.length == 0) {
        table.append("<thead></thead>");
        thead = table.find("thead");
    }

    var html = [];
    html.push("<tr>");
    $.each(_self.columns, function (index, column) {
        if (column.Display) {
            html.push("<th");
            if (column.FieldClass != undefined && column.FieldClass.length > 0) {
                html.push(" class='" + column.FieldClass + "'");
            }
            if (column.Sortable == 0) {
                html.push(">" + column.FieldTitle);
            }
            else {
                var sortClass = "sortLink";
                var sortName = column.SortName;
                if (sortName == undefined || sortName.length == 0){
                    sortName = column.FieldName;
                }

                if (sortName == _self.setting.sortName) {
                    sortClass += " sort-" + _self.setting.sortOrder;
                }

                html.push("><a href=\"###\" class=\"" + sortClass + "\"");
                if (sortName != undefined && sortName.length > 0) {
                    html.push(" data-field='" + sortName + "'");
                }
                html.push(">" + column.FieldTitle + "<span class=\"sort\"></span>");
            }
            html.push("</th>");
        }
    });
    html.push("</tr>");
    thead.html(html.join(''));

    $(".sortLink").click(function () {
        var sortMode = _self.setting.sortOrder;
        if (sortMode == "asc") {
            sortMode = "desc";
        }
        else {
            sortMode = "asc";
        }
        _self.setting.sortOrder = sortMode;
        _self.setting.sortName = $(this).attr("data-field");
        _self.bindHeader();
        _self.binbBody();
        return true;
    });
};

nhhui.nhhtable2.prototype.binbBody = function () {
    var _self = this;
    var params = {};
    params.paging = 1;
    params.page = _self.setting.page;
    if (_self.setting.sortName != undefined) {
        params.orderMode = _self.setting.sortOrder;
        params.orderBy = _self.setting.sortName;
    }
    var queryData = _self.setting.queryParams(params);
    var table = _self.container.find("table");
    var tbody = table.find("tbody");
    if (tbody == undefined || tbody.length == 0) {
        table.append("<tbody></tbody>");
        tbody = table.find("tbody");
    }

    var paging = _self.container.find(".paging");
    if (paging == undefined || paging.length == 0) {
        _self.container.append("<ul class=\"pagination pull-right no-margin paging\"></ul>");
    }

    $.ajax({
        url: _self.setting.url,
        type: _self.setting.type,
        data: queryData,
        dataType: "JSON",
        success: function (result, status) {
            console.log(status);
            var html = []
            $.each(result.List, function (index, item) {
                html.push("<tr>");
                $.each(_self.columns, function (i, column) {
                    if (column.Display) {
                        html.push("<td>");
                        if (column.Formatter == undefined || column.Formatter.length == 0) {
                            html.push(item[column.FieldName]);
                        }
                        else {
                            html.push(window[column.Formatter](item));
                        }
                        html.push("</td>");
                    }
                });
                html.push("</tr>");
            });
            tbody.html(html.join(''));
            _self.pagingInfo = result.PagingInfo;
            _self.bindPaging();
        }
    });
};

nhhui.nhhtable2.prototype.bindPaging = function () {
    var _self = this;
    if (_self.pagingInfo == undefined)
        return;
    var _pagingInfo = _self.pagingInfo;

    var html = [];
    html.push("<li style=\"float:left; margin-right:10px; line-height:30px;\">");
    html.push("查询到 " + _pagingInfo.TotalCount + "条记录，共 " + _pagingInfo.PageCount + "页");
    html.push("</li>");
    if (_pagingInfo.PageIndex != 1) {
        html.push("<li class=\"next\"><a href=\"javascript:void(0);\" class=\"page\" data-page=\"1\">第一页</a></li>");
    }
    if (_pagingInfo.PageIndex > 1) {
        html.push("<li class=\"prev\"><a href=\"javascript:void(0);\" class=\"page\" data-page=\"" + (_pagingInfo.PageIndex - 1) + "\">上一页</a></li>");
    }
    for (var page = _pagingInfo.StartPage; page <= _pagingInfo.EndPage; page++) {
        html.push("<li");
        if (_pagingInfo.PageIndex == page) {
            html.push(" class=\"active\"");
        }
        html.push("><a href=\"javascript:void(0);\" class=\"page\" data-page=\"" + page + "\">" + page + "</a></li>");
    }

    if (_pagingInfo.PageIndex < _pagingInfo.PageCount) {
        html.push("<li class=\"next\"><a href=\"javascript:void(0);\" class=\"page\" data-page=\"" + (_pagingInfo.PageIndex + 1) + "\">下一页</a></li>");
    }
    if (_pagingInfo.PageIndex != _pagingInfo.PageCount) {
        html.push("<li class=\"next\"><a href=\"javascript:void(0);\" class=\"page\" data-page=\"" + _pagingInfo.PageCount + "\">最后一页</a></li>");
    }
    _self.container.find(".paging").html(html.join(''));

    $(".page").click(function () {
        _self.setting.page = $(this).attr("data-page");
        _self.binbBody();
        return true;
    });
};

nhhui.nhhtable2.prototype.initSearch = function () {
    var _self = this;
    if (_self.setting.searchBtn.length == 0)
        return false;
    $("#" + _self.setting.searchBtn).click(function () {
        _self.binbBody();
    });
};

nhhui.nhhtable2.prototype.initExport = function () {
    var _self = this;
    if (_self.setting.exportBtn.length == 0)
        return false;
    if (_self.setting.exportUrl == undefined || _self.setting.exportUrl.length == 0)
        return false;

    $("#" + _self.setting.exportBtn).click(function () {
        var params = {};
        params.reportCode = _self.setting.reportCode;
        var queryParams = _self.setting.queryParams(params);
        window.open(_self.setting.exportUrl + "?" + $.param(queryParams));
    });
};

nhhui.alert = function (msg, title) {
    if (title == undefined || title.length == 0) {
        title = "立天唐人智能商业管理平台";
    }
    var alertbox = $("#alertbox");
    if (alertbox.length == 0) {
        var strModal = "<div id='alertbox' class='modal fade' tabindex='-1' aria-hidden='true' style='display: none;'>";
        strModal += "<div class='modal-dialog'><div class='modal-content'><div class='modal-header no-padding'>";
        strModal += "<div class='table-header'>";
        strModal += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'><span class='white'>×</span></button>";
        strModal += "<span id='alertbox-title'></span></div></div>";
        strModal += "<div class='modal-body nothing' id='alertbox-body'></div>";
        strModal += "<div class='ui-btngroup'><a href='###' class='btn btn-sm btn-success' data-dismiss='modal' aria-hidden='true'>确定</a></div>";
        strModal += "</div></div></div>";
        alertbox = $(strModal);
        $(document.body).append(alertbox);
    }
    $("#alertbox-title").text(title);
    $("#alertbox-body").html(msg);
    $(alertbox).modal();
};
window.alert = nhhui.alert;

nhhui.confirm = function (msg, func1, func2, title) {
    if (title == undefined || title.length == 0) {
        title = "立天唐人智能商业管理平台";
    }
    var confirmbox = $("#confirmbox");
    if (confirmbox.length == 0) {
        var strModal = "<div id='confirmbox' class='modal fade' tabindex='-1' aria-hidden='true' style='display: none;'>";
        strModal += "<div class='modal-dialog'><div class='modal-content'><div class='modal-header no-padding'>";
        strModal += "<div class='table-header'>";
        strModal += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'><span class='white'>×</span></button>";
        strModal += "<span id='confirmbox-title'></span></div></div>";
        strModal += "<div class='modal-body nothing' id='confirmbox-body'></div>";
        strModal += "<div class='ui-btngroup'><a href='###' class='btn btn-sm btn-success mr20' id='btn-confirmbox-yes'>确定</a>";
        strModal += "<a href='###' class='btn btn-sm btn-warning' id='btn-confirmbox-no'>取消</a></div>";
        strModal += "</div></div></div>";
        confirmbox = $(strModal);
        $(document.body).append(confirmbox);
    }
    $("#confirmbox-title").text(title);
    $("#confirmbox-body").html(msg);
    $(confirmbox).modal();
    $("#btn-confirmbox-yes").click(function () {
        confirmbox.modal("hide");
        if (func1 != undefined) { func1(); }
        return true;
    });
    $("#btn-confirmbox-no").click(function () {
        confirmbox.modal("hide");
        if (func2 != undefined) { func2(); }
        return false;
    });
};
window.confirm = nhhui.confirm;

nhhui.loading = function (msg, title) {
    if (title == undefined || title.length == 0) {
        title = "立天唐人智能商业管理平台";
    }
    var loadingbox = $("#loadingbox");
    if (loadingbox.length == 0) {
        var strModal = "<div id='loadingbox' class='modal fade' tabindex='-1' style='display: none;'>";
        strModal += "<div class='modal-dialog'><div class='modal-content'><div class='modal-header no-padding'>";
        strModal += "<div class='table-header'>";
        strModal += "<span id='loadingbox-title'></span></div></div>";
        strModal += "<div class='modal-body nothing' style='padding: 50px 20px;' id='loadingbox-body'></div>";
        strModal += "</div></div></div>";
        loadingbox = $(strModal);
        $(document.body).append(loadingbox);
    }
    $("#loadingbox-title").text(title);
    $("#loadingbox-body").html("<img src='/Content/assets/img/loading.gif' style='margin-right: 10px;' />" + msg);
};
nhhui.loading.prototype = {
    show: function () {
        $("#loadingbox").modal({ backdrop: 'static', keyboard: false });
    },
    hide: function () {
        $("#loadingbox").modal("hide");
    }
};
window.loading = nhhui.loading;