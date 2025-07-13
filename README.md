# Local Sharing Center

## Overview

Local Sharing Center is a tool used to transfer files from one PC to another. There are two types of this tool, the first one is the main version, which uses a multi-client server that clients can connect to, ideally used in offices
where there is a need to share a file with every computer of an employee, with security in mind as well, more than that, to upload files to the server as well.
The second version is the home version which is a simpler version used to transfer files between two computers directly in a setup, for example: downloading a file to my PC from my laptop.

Hope you enjoy using it!

## Features

Main version

- Server discovery using UDP broadcast.
- Secure connection between client and server using RSA and AES.
- SQLite database for user storage for both users and admins, stores the password using SHA-256 + salt.
- Secure server authentication by RSA signature, used to prevent spoofing.
- Simple anti-DDoS system for the server with ban system.
- Custom protocol for connection, authentication and file transfer.
- Supports file transfer of any file with no size limit.
- Real-time logs for both client and server.
- Multiple server prevention.
- Multithreaded server.
- Secure file transfer using AES.
- Login/ sign-up system with strict password policy.
- Bulk download, for example: 5 files from the list.

 Home version

- All core aspects.
- No login/ sign-up screens.
- Simpler design and reduced security options.

## Prerequisites

  - Windows machine.

## Installation

To use the local sharing center, follow these steps:

1. Download the latest release version MSI installer.
2. Run the MSI installer.
3. Enjoy.

## Usage

## Main
For Server:
run the executable file and select "Server", if there aren't any, your computer will be used as a server.
Log in using the admin username and password.
You'll see logs of the current processes, who logged in, and downloaded what.

For Client:
run the executable file and select "Client", first it will search for a server, if there is one select between sign-up or login.
If you chose sign-up then write your username and password according to the policy, if successful, you can log in.
For login, press the button and log in with your credentials.
Next you will see the control panel which has a few options:
1. List - View available files from the server.
2. Download - to download selected files (can be dragged with the mouse).
3. Upload File - to upload a file to the server.
4. Disconnect - to disconnect from the server.
5. Select All - to select to download all of the files.
If the shared folder, which contains the files doesn't exist, it will be created.

## Home
Same as the main version, but without login or sign-up.
