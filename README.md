# FolderMonitor
Folder Monitor is a windows service for sync files and folders with nice GUI, it uses the robocopy tool.

FolderMonitor is more than a robocopy, it extends its functionally to work with protected paths, so you can assign a username and password, also, it resolves the un-join domain devices and give ability to connect to remote UNC shares using a username and password.
FolderMonitor works as a windows service to make sure that your copy tasks work even if you didn't login to windows.

<img src=/docs/mainwindow_0.8.PNG />

# Add New Copy Task
Use this dialog to add new copy task, as you see you have a lot of option, to give you ability to do what you desired.

<img src=/docs/editTask.PNG />

# Access Cradentials
You have the ability to provide the username and password for every protected directory.

<img src=/docs/Path_cradentials.PNG />

# Run as Windows Service
You can manage the foldermonitor service, so you don't worry about login to windows, just start your computer and all your copy tasks will work.

<img src=/docs/service_config_main.PNG />

