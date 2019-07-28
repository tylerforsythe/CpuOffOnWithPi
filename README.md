CPU Off/On With Raspberry Pi
============================

This solution has 2 projects intended to allow you to know the status of an control a machine's 
power state with a Raspberry Pi. It's intended to run on Windows but could probably be easily adapted 
for other platforms. Also, it could probably be adapted to not even need to be run on a Pi, but 
I like it that way.

Originally I intended it to be hooked up to the power switch of the CPU via the Pi GPIO headers, but 
the CPU EXE is intended to make that need obsolete.

Nutshell to get it to work:

* CpuExe running on each Windows machine
* WebAPI running on a Pi that is the brains of the whole thing
* Website also running on the Pi and server by the WebAPI project
* Pi that runs the WebAPI must be running Mono version 6+. It may work in earlier versions, but definitely not 4.6.X.

CpuExe
------

Inteded to run on the machine you want to control, automatically starting at boot with full admin privileges. 
Or at least enough to invoke a shutdown command. This will communicate the status of the machine IF it's up 
and possibly other details, like CPU usage. It's main purpose is to respond to a "shutdown" command by invoking a
native process to tell the OS to shutdown. It runs a web service with super-basic HTML site (just a test, really)
and an API that can respond to Shutdown, and probably other status commands later.

Web
---

The web project is the GUI that you'll interact with in your browser. Written in HTML/JS with the Angular framework, 
the purpose is to give an easy to use interface to see if the machine is up and make it shutdown or turn on.

Intention is to build this so that I can have it controlling multiple machines from one Pi web host, that will be 
running this project and talking to the CpuExe's on other [Windows] machines.

SelfUpdate
----------

I use this connected with the WebAPI (and soon CpuExe) so that, using a local network share, I can have the app
update and restart itself without me having to kill it, copy files over, and then restart it on the pi. That's annoying.
This automates the whole thing with Visual Studio post-build events, a PowerShell, and small bit of logic in the API.
