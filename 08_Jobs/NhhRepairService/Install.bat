%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil D:\NHH\03_Coding\01_SourceCode\07_Jobs\NhhRepairService\bin\Debug\NhhRepairService.exe
Net Start NhhRepairService

sc config NhhRepairService start= auto

pause