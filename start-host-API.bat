@echo off
mode con: cols=120 lines=5
echo Starting Web.API...
CD /D "H:\FULL\BETA_2\IPG.Projeto\IPG.Projeto.API"

SET ASPNETCORE_ENVIRONMENT=Development
SET ASPNETCORE_URLS=http://192.168.1.68:56699
dotnet run