Option Explicit

Dim user
Dim password

user = Wscript.Arguments(0)
password = Wscript.Arguments(1)


Msgbox("user is " & user & vbcrlf & _
	"password is " & password)
