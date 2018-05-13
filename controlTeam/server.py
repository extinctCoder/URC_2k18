# -*- coding: utf-8 -*-
"""
Created on Wed May 09 05:59:00 2018

@author: darthVader
"""

import socket
ip = '127.0.0.1'
port = 8888
BUFFER_SIZE = 10
def printMsg(msg):
    print msg
    return

printMsg("welcome to py socket script")
printMsg("trying to open a port")
server = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
printMsg("server created")
try:
    server.bind((ip,port))
except socket.error as ex:
    printMsg(ex[1])
else:
    printMsg("server bind successfull")
    while(1):
        try:
            server.listen(10)
            while(1):
                try:
                    printMsg("waiting for the gui")
                    con, add = server.accept()
                    printMsg(add[1])
                except server.error as ex:
                    printMsg(ex[1])
                else:
                    printMsg("sending data to gui")
                    while(1):
                        con.sendall("data is being send\n")
                        printMsg("data is being send\n")
        except:
            printMsg("gui offline")
        finally:
            server.close()
finally:
    printMsg("thank you")