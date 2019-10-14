Passform
====

## Overview
This tool is writen by C#.

## Description
1. First argument of passform.exe(username) and Enterd Passwords can be passed as arguments of executable file.

2. Passform can display masked passwords.

3.  csc.exe is necessary to execute the Passform in the execution environment.

## VS
Unlike `Get-Credential` on PowerShell,Passform can check passwords being enterd.

## Requirement

* Windows10
* .NET Framework
* csc.exe

## Usage

1. Execute this command to compile the program.   
`csc /t:winexe passform.cs`

2.  Execute with arguments specified.  
`passform.exe [username] [executable file]`
3.  After entering the passwords,do the following processing in the program.
`executable file [username] [password]`

## License
[MIT](https://github.com/zattuzad/passform/blob/master/LICENSE)

## Author
[zattuzad](https://github.com/zattuzad)

