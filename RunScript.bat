ECHO off
sqlcmd -S localhost -E -i MarwaCar.sql
ECHO .
ECHO if no errors appear DB was created
PAUSE