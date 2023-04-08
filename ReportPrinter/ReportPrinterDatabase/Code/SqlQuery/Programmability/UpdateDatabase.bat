@ECHO off

REM -------------------------------------------------------------------------------------------------------
REM  Remove Existing file list and Creating new one based on the SQL files available including sub folders
REM --------------------------------------------------------------------------------------------------------

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
REM                 Set Server and Database
REM ----------------------------------------------------------------------------------

SET server=LAPTOP-ETHALNIF
SET database=ReportPrinterTest
SET _sqllist=list.txt

ECHO ======================================== Update %database% database ========================================
ECHO.

FOR /f "tokens=*" %%f in (%_sqllist%) do (
    ECHO Executing file "%%f"
    FOR %%A in ("%%f") do (
        sqlcmd -S %server% -d %database% -i "%%f"
    )
)

ECHO.
ECHO ======================================== Update %database% database ========================================
ECHO.

SET database=ReportPrinter

FOR /f "tokens=*" %%f in (%_sqllist%) do (
    ECHO Executing file "%%f"
    FOR %%A in ("%%f") do (
        sqlcmd -S %server% -d %database% -i "%%f"
    )
)

SET _sqllist=
ECHO.
ECHO Process completed. Please verify the scripts executed in list.txt file.
PAUSE