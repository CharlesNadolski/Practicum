echo off
echo Building MealService.sln...
msbuild.exe MealService.sln /v:minimal /t:Build /p:Configuration=Debug;Platform="Any CPU"
echo Running unit and integration tests...
nunit-console.exe .\MealService.Tests\bin\Debug\MealService.Tests.dll /exclude=XmlGenerator
echo on