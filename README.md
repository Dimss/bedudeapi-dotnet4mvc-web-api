### Run build for Azure Cloud Service 
```
MSBuild.exe /t:Publish /p:TargetProfile=Cloud /p:AutomatedBuild=True /p:Configuration=Release`
```