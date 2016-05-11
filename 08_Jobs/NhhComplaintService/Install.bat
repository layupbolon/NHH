%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil D:\NHH\03_Coding\01_SourceCode\07_Jobs\NhhComplaintService\bin\Debug\NhhComplaintService.exe

Net Start NhhComplaintService

sc config NhhComplaintService start= auto

pause