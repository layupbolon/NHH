%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil D:\NHH\03_Coding\01_SourceCode\08_Jobs\NhhCampaignService\NhhCampaignService\bin\Debug\NhhCampaignService.exe
Net Start NhhCampaignService

sc config NhhCampaignService start= auto

pause