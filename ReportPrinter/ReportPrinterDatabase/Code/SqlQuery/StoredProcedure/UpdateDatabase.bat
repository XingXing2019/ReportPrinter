@ECHO off

REM -------------------------------------------------------------------------------------------------------
REM  Remove Existing file list and Creating new one based on the SQL files available including sub folders
REM --------------------------------------------------------------------------------------------------------

ECHO Deploy stored procedures on ReportPrinterTest database. 
ECHO.

IF EXIST list.txt DEL /Q .\list.txt
FOR /f %%i in ('DIR *.sql /s /b 2^> nul ^| FIND "" /v /c') do SET FileCount=%%i

IF "%FileCount%"=="0" (
    ECHO There is no SQL files to execute on current and sub directory.
    ECHO.
    GOTO commonexit
)

IF NOT "%FileCount%"=="0" (
    DIR *.sql /b /s /a-d >> list.txt
)


REM ----------------------------------------------------------------------------------
REM                 Collecting Server, Database and Credential
REM ----------------------------------------------------------------------------------

SET server=LAPTOP-ETHALNIF
SET catalog=ReportPrinterTest
SET _sqllist=list.txt

FOR /f "tokens=*" %%f in (%_sqllist%) do (
    ECHO Executing file "%%f"
    FOR %%A in ("%%f") do (
        sqlcmd -S %server% -d %catalog% -i "%%f"
    )
)

ECHO.
ECHO Deploy stored procedures on ReportPrinter database. 
ECHO.

SET catalog=ReportPrinter

FOR /f "tokens=*" %%f in (%_sqllist%) do (
    ECHO Executing file "%%f"
    FOR %%A in ("%%f") do (
        sqlcmd -S %server% -d %catalog% -i "%%f"
    )
)

SET _sqllist=
ECHO.
ECHO Process completed. Please verify the scrits executed in list.txt file.
ECHO.
GOTO commonexit

:commonexit
PAUSE