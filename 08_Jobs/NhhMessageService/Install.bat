%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil D:\NHH\03_Coding\01_SourceCode\07_Jobs\NhhMessageService\bin\Debug\NhhMessageService.exe
Net Start NHHMessageService

sc config NHHMessageService start= auto

pause