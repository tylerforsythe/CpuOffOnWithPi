CPU Off/On With Raspberry Pi
============================

This solution has 3 projects intended to allow you to know the status of an control a machine's 
power state with a Raspberry Pi. It's intended to run on Windows but could probably be easily adapted 
for other platforms. Also, it could probably be adapted to not even need to be run on a Pi, but 
I like it that way.

Originally I intended it to be hooked up to the power switch of the CPU via the Pi GPIO headers, but 
the CPU EXE is intended to make that need obsolete.

CLI
---

The CLI app runs on the Raspberry Pi and basically just continually checks the status of the machine and 
outputs the data to files that the web server can read. I don't want the web server to be doing these reads.

I may be able to make this completely unncessary if the web server is able to rapidly talk to the CpuExe.

CpuExe
------

Inteded to run on the machine you want to control, automatically starting at boot with full admin privileges. 
Or at least enough to invoke a shutdown command. This will communicate the status of the machine IF it's up 
and possibly other details, like CPU usage. It's main purpose is to respond to a "shutdown" command by invoking a
native process to tell the OS to shutdown.

Web
---

The web project is the GUI that you'll interact with in your browser. Written in HTML/JS with the Angular framework, 
the purpose is to give an easy to use interface to see if the machine is up and make it shutdown or turn on.

Intention is to build this so that I can have it controlling multiple machines from one Pi, which is simple if 
we don't need to wire into the power button.
