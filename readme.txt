-----------------
About the Samples
-----------------

This archive includes the sample code for the book
"Peer-to-Peer with VB .NET."

For other related examples, and to download the most recent code
(which may include corrections or additional examples in the future),
visit http://www.prosetech.com.


-----------------------------
Testing in Visual Studio .NET
-----------------------------

Many examples require more than one application. In some cases, you can
run and debug all the applications at the same time in Visual Studio .NET.
In these cases, the solution with include all the projects, and will be
configured to launch them in the correct order. In some cases, however,
this is not possible. (Examples include when one application is a Windows Service,
or if you need to run multiple instances of the same application.) In these
cases, you will need to run some applications directly from the command-line or
through Windows Explorer.


------------------------------------
Visual Studio .NET 2003 and .NET 1.1
------------------------------------

The code samples are provided as Visual Studio .NET projects, which
means you can open them in Visual Studio .NET or Visual Studio .NET 2003.
When opening a project in Visual Studio .NET 2003, you will asked to
convert to the files to the newer format. This process will take place
automatically when you accept, and after this point the project will only
be accessible in Visual Studio .NET 2003. 

Visual Studio .NET 2003 targets the .NET 1.1 platform. All the code in this
book has been tested and works equally well with .NET 1.0 and 1.1. In a
few cases, you will need to modify the configuration files to enable
full serialization support with Remoting in .NET 1.1. (The lines you need
are included in the configuration file, but commented out by default.)
Consult the following list of examples for information about
when this change is required.


-----------
Sample List
-----------

The following list identifies each project and describes any special
considerations that there are for using it.

---------------------------------------------------------------------------

\Chapter03\OneWayRemoting\
* Demonstrates a client that activates a server-side object.

\Chapter03\TwoWayRemoting\
* Demonstrates a client that calls a server-side object, disconnects, and
is contacted later by the server-side object.

---------------------------------------------------------------------------

\Chapter04\Talk .NET\
* The first Talk .NET version. All communication is routed through the
server.
* Using Visual Studio .NET, you can run the server and one client. You can
then send messages to yourself. To test a multi-user scenario, start other
TalkClient instances outside of Visual Studio .NET after starting the server.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

\Chapter04\Talk .NET Decentralized\
* The second Talk .NET version. Peers contact one another directly.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

\Chapter04\Talk .NET File Transfer\
* A hybrid Talk .NET that supports file transfers. Messages are sent through
the server but file transfer is peer-to-peer.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

---------------------------------------------------------------------------

\Chapter05\Talk .NET\
* A Talk .NET version that uses the asynchronous message delivery service.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

\Chapter05\TalkNetService\
* A Windows Service that can be used instead of the stand-alone TalkServer
application. In order to use this component, you must install and start it in
the SCM, as described in the book at the end of Chapter 5.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

---------------------------------------------------------------------------

\Chapter06\PrimeNumbers\
* A work-sharing application for calculating prime numbers.
* The worker and server applications are contained in separate projects.
To test, start the server, and then start one or more workers. (To start more
than one worker, you will need to run the additional workers outside of Visual
Studio .NET.)
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client and server app.config files.

\Chapter06\DynamicAssemblyLoad\
* Loads an assembly into a sandbox, with limited privileges. The source for
the dynamically loaded assembly is found in the TaskComponent project.

---------------------------------------------------------------------------

\Chapter07\TCP\
* Tests communication over a TCP channel where one application is designated
as the server. You can run the solution in Visual Studio .NET to load both
applications.

\Chapter07\UDP\
* Tests communication over a UDP channel. To test, you must load two instances
of the same project, as described in the book.

---------------------------------------------------------------------------

\Chapter08\Discovery\
* A discovery service for song sharing over TCP connections. To use this
service with the peer-to-peer application in the next chapter, you must map
this directory to the virtual directory http://localhost/Discovery.
* Before using the discovery service, you must install the P2P database.
You will require SQL Server 7 or later. To install the database, begin by
creating an empty database named P2P. Then, load Query Analyzer, and select
Query --> Change Database from the menu. Choose the newly created P2P database.
Then select File --> Open, and open the P2P.sql file in the \Chapter08\ directory.
Choose Query --> Execute to run it. This will create the empty tables, and
the required stored procedures.

---------------------------------------------------------------------------

\Chapter09\FileSwapper\
* A file sharing application that uses TCP connections and the discovery
service from the previous chapter.
* Before testing, you must place one or more MP3 files in the \bin\
subdirectory.
* You can test with a single instance (by downloading your own files),
but it will be easier to test with two or more instances.
In this case, place each instance in its own directory, and give
each directory a different set of MP3 files to make testing more
straightforward.

---------------------------------------------------------------------------

\Chapter11\SecureDiscovery\
* A modified discovery service that assumes communication via Remoting
and requires users to authenticate themselves when signing in. To use this
service with the Seucre Talk .NET application, you must map this directory
to the virtual directory http://localhost/SecureDiscovery.
* Before using this discovery service, you must install the SecureP2P database.
You will require SQL Server 7 or later. To install the database, begin by
creating an empty database named SecureP2P. Then, load Query Analyzer, and select
Query --> Change Database from the menu. Choose the newly created SecureP2P
database. Then select File --> Open, and open the SecureP2P.sql file in the
\Chapter11\ directory. Choose Query --> Execute to run it. This will create the
empty tables, and the required stored procedures.


\Chapter11\Secure Talk.NET\
* A version of the Talk .NET that uses the secure service for peer discovery,
and sends encrypted messages between peers.
* If you are using with .NET 1.1, uncomment the <serverProviders> section
in the client app.config file.

---------------------------------------------------------------------------

\Chapter12\Messenger\
* This example integrates with Windows Messenger. To use it, you must have
Windows Messenger installed (find it at http://www.msn.com, or included with
some versions of the Windows operating system).
* This example also uses the MSN Helper API. For updated versions,
documentation, and bugs, see http://msnphelper.sourceforge.net.
* To perform a proper test, you will need two Hotmail accounts that are
registered on each other's contact list. Sign in to one account using
Windows Messenger. Then, sign in to the other using the simple Messenger
test application (you will need to modify the code to use this account).
You should then be able to start a session using the test application,
and exchange messages between the two.

\Chapter12\GrooveTool\
* This example uses the Groove platform. To use it, you must have installed
the Groove deloper kit for .NET, as described in the book.

---------------------------------------------------------------------------

\Chapter13\IntelP2P Talk .NET\
* Includes a version of Talk .NET that uses the Intel P2P Accelerator kit.
* You must download and install the Intel P2P kit before using this example.
You can find it as a separate download at http://www.prosetech.com.


