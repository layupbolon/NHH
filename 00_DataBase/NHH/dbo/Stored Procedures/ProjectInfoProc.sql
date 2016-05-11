CREATE proc [dbo].[ProjectInfoProc]
(
  @name varchar(100),
  @locationID int,
  @provinceID int,
  @stage int,
  @cityid int,
  @openTime  varchar(20)
  --@roomTypeM varchar(20),
  --@roomTypeS varchar(20)
)as 
begin
declare @tmpsql varchar(max)
declare @roomTypeM varchar(20)
declare @roomTypeS varchar(20)
set @roomTypeM='主力店'
set @roomTypeS='步行街'
select @tmpsql=' with cte as(

  select ProjectID,sum(TotalConstructionArea) as SumTotalConstructionArea ,sum(TotalRentArea) as TotalRentArea
         from [NHH].[dbo].[Project_Building]
         group by ProjectID 
		 )
SELECT   pro.ProjectID, pro.ProjectName, pro.Location, pro.OwnerCompanyID, pro.ProvinceID, pro.CityID, pro.Stage, 
                pro.GrandOpeningDate,pro.ParkingLotNum,cte.SumTotalConstructionArea,cte.TotalRentArea,
				cp.CompanyName,sp.ProvinceName,sc.CityName,pro.AdPointNum as AdPointNum
FROM      dbo.Project AS pro
          inner join cte on cte.ProjectID=pro.ProjectID 
          inner join dbo.Project_Unit pu on cte.ProjectID=pu.ProjectID
		  left join dbo.Company cp on pro.OwnerCompanyID=cp.CompanyID
		  left join dbo.S_Province sp on sp.ProvinceID=pro.ProvinceID
		  left join dbo.S_City sc on sc.CityID=pro.CityID
		  where pu.Tag LIKE '''+@roomTypeM+''' or pu.Tag LIKE '''+@roomTypeS+''' and 1=1'
if(@name is not null and @name!='')
   select @tmpsql=@tmpsql+' and pro.ProjectName like '+@name
 if(@locationID is not null and @locationID!=0)
    select @tmpsql=@tmpsql+' and pro.Location='+cast(@locationID as varchar(15))
if(@provinceID is not null and @provinceID!=0)
   select @tmpsql=@tmpsql+' and pro.ProvinceID='+cast(@provinceID as varchar(15))
if(@cityid is not null and @cityid!=0)
   select @tmpsql=@tmpsql+' and pro.CityID='+cast(@cityid as varchar(25))
if(@stage is not null and @stage!=0)
  select @tmpsql=@tmpsql+' and pro.Stage='+cast(@stage as varchar(15))
  if(@openTime is not null )
  select @tmpsql=@tmpsql+' and pro.GrandOpeningDate='+@openTime 
    return exec(@tmpsql)
   --print(@tmpsql)
   --return @tmpsql
end

