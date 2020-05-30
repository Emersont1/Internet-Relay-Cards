using System;

namespace LibIRC {
    /// <summary>
    /// The Error Codes for IRC Taken from p43-55 of https://www.rfc-editor.org/rfc/rfc1459.txt
    /// </summary>
    public enum ErrorCode {
        /// <summary>
        /// Used to indicate the nickname parameter supplied to a command is currently unused.
        /// </summary>
        NoSuchNick = 401,

        /// <summary>
        /// Used to indicate the server name given currently doesn't exist.
        /// </summary>
        NoSuchServer = 402,

        /// <summary>
        /// Used to indicate the given channel name is invalid.
        /// </summary>
        NoSuchChannel = 403,

        /// <summary>
        /// Sent to a user who is either
        /// (a) not on a channel which is mode +n or
        /// (b) not a chanop (or mode +v) on a channel which has mode +m set
        /// and is trying to send PRIVMSG message to that channel.
        /// </summary>
        CannotSendToChannel = 404,

        /// <summary>
        /// Sent to a user when they have joined the maximum number of allowed channels and they try to join ,another channel.
        /// </summary>
        TooManyChannels = 405,

        /// <summary>
        /// Returned by WHOWAS to indicate there is no history information for that nickname.
        /// </summary>
        WasNoSuchNick = 406,

        /// <summary>
        /// Returned to a client which is attempting to send a PRIVMSG/NOTICE using the user@host destination format and for a user@host which has several occurrences.
        /// </summary>
        TooManyTargets = 407,

        /// <summary>
        /// PING or PONG message missing the originator parameter which is required since these commands must work without valid prefixes.
        /// </summary>
        NoOrigin = 409,

        /// <summary>
        /// are returned by PRIVMSG to indicate that the message wasn't delivered for some reason. ERR_NOTOPLEVEL and ERR_WILDTOPLEVEL are errors that are returned when an invalid use of <c>PRIVMSG $&lt;server&gt;</c> or <c>PRIVMSG #&lt;host&gt;</c> is attempted.
        /// </summary>
        NoRecipient = 411,
        /// <summary>
        /// are returned by PRIVMSG to indicate that the message wasn't delivered for some reason. ERR_NOTOPLEVEL and ERR_WILDTOPLEVEL are errors that are returned when an invalid use of <c>PRIVMSG $&lt;server&gt;</c> or <c>PRIVMSG #&lt;host&gt;</c> is attempted.
        /// </summary>
        NoTextToSend = 412,
        /// <summary>
        /// are returned by PRIVMSG to indicate that the message wasn't delivered for some reason. ERR_NOTOPLEVEL and ERR_WILDTOPLEVEL are errors that are returned when an invalid use of <c>PRIVMSG $&lt;server&gt;</c> or <c>PRIVMSG #&lt;host&gt;</c> is attempted.
        /// </summary>
        NoTopLevel = 413,
        /// <summary>
        /// are returned by PRIVMSG to indicate that the message wasn't delivered for some reason. ERR_NOTOPLEVEL and ERR_WILDTOPLEVEL are errors that are returned when an invalid use of <c>PRIVMSG $&lt;server&gt;</c> or <c>PRIVMSG #&lt;host&gt;</c> is attempted.
        /// </summary>
        WildTopLevel = 414,

        /// <summary>
        /// Returned to a registered client to indicate that the command sent is unknown by the server.
        /// </summary>
        UnknownCommand = 421,

        /// <summary>
        /// Server's MOTD file could not be opened by the server.
        /// </summary>
        NoMotd = 422,

        /// <summary>
        /// Returned by a server in response to an ADMIN message when there is an error in finding the appropriate information.
        /// </summary>
        NoAdminInfo = 423,

        /// <summary>
        /// Generic error message used to report a failed file operation during the processing of a message.
        /// </summary>
        FileError = 424,

        /// <summary>
        /// Returned when a nickname parameter expected for a command and isn't found
        /// </summary>
        NoNicknamegiven = 431,

        /// <summary>
        /// Returned after receiving a NICK message which contains characters which do not fall in the defined set.  See section x.x.x for details on valid nicknames.
        /// </summary>
        ErroneusNickname = 432,

        /// <summary>
        /// Returned when a NICK message is processed that results in an attempt to change to a currently existing nickname.
        /// </summary>
        NicknameInUse = 433,

        /// <summary>
        /// Returned by a server to a client when it detects a nickname collision (registered of a NICK that already exists by another server).
        /// </summary>
        NickCollision = 436,

        /// <summary>
        /// Returned by the server to indicate that the target user of the command is not on the given channel.
        /// </summary>
        UserNotInChannel = 441,

        /// <summary>
        /// Returned by the server whenever a client tries to perform a channel effecting command for which the client isn't a member.
        /// </summary>
        NotOnChannel = 442,

        /// <summary>
        /// Returned when a client tries to invite a user to a channel they are already on.
        /// </summary>
        UserOnChannel = 443,

        /// <summary>
        /// Returned by the summon after a SUMMON command for a user was unable to be performed since they were not logged in.
        /// </summary>
        NoLogin = 444,

        /// <summary>
        /// Returned as a response to the SUMMON command.  Must be returned by any server which does not implement it.
        /// </summary>
        SummonDisabled = 445,

        /// <summary>
        /// Returned as a response to the USERS command.  Must be returned by any server which does not implement it.
        /// </summary>
        UsersDisabled = 446,

        /// <summary>
        /// Returned by the server to indicate that the client must be registered before the server will allow it to be parsed in detail.
        /// </summary>
        NotRegistered = 451,

        /// <summary>
        /// Returned by the server by numerous commands to indicate to the client that it didn't supply enough parameters.
        /// </summary>
        NeedMoreParams = 461,

        /// <summary>
        /// Returned by the server to any link which tries to change part of the registered details (such as password or user details from second USER message).
        /// </summary>
        AlreadyProtected = 462,

        /// <summary>
        /// Returned to a client which attempts to register with a server which does not been setup to allow connections from the host the attempted connection is tried.
        /// </summary>
        NoPermFromHost = 463,

        /// <summary>
        /// Returned to indicate a failed attempt at registering a connection for which a password was required and was either not given or incorrect.
        /// </summary>
        PasswordMismatch = 464,

        /// <summary>
        /// Returned after an attempt to connect and register yourself with a server which has been setup to explicitly deny connections to you.
        /// </summary>
        YoureBannedCreep = 465,

        /// <summary>
        /// Channel key already set
        /// </summary>
        KeySet = 467,

        /// <summary>
        /// Cannot join channel (+l)
        /// </summary>
        ChannelIsFull = 471,

        /// <summary>
        /// is unknown mode char to me
        /// </summary>
        UnknownMode = 472,

        /// <summary>
        /// Cannot join channel (+i)
        /// </summary>
        InviteOnlyChannel = 473,

        /// <summary>
        /// Cannot join channel (+b)
        /// </summary>
        BannedFromChannel = 474,

        /// <summary>
        /// Cannot join channel (+k)
        /// </summary>
        BadChannelKey = 475,

        /// <summary>
        /// Any command requiring operator privileges to operate must return this error to indicate the attempt was unsuccessful.
        /// </summary>
        NoPrivileges = 481,

        /// <summary>
        /// Any command requiring 'chanop' privileges (such as MODE messages) must return this error if the client making the attempt is not a chanop on the specified channel.
        /// </summary>
        ChannelOpPrivsNeeded = 482,
        /// <summary>
        /// Any attempts to use the KILL command on a server are to be refused and this error returned directly to the client.
        /// </summary>
        CantKillServer = 483,

        /// <summary>
        /// If a client sends an OPER message and the server has not been configured to allow connections from the client's host as an operator, this error must be returned.
        /// </summary>
        NoOperHost = 491,

        /// <summary>
        /// Returned by the server to indicate that a MODE message was sent with a nickname parameter and that the a mode flag sent was not recognized.
        /// </summary>
        UnknownModeFlag = 501,

        /// <summary>
        /// Error sent to any user trying to view or change the user mode for a user other than themselves.
        /// </summary>
        UsersDontMatch = 502,

        /// <summary>
        /// Dummy Reply Number. Not Used
        /// </summary>
        None = 300,

        /// <summary>
        /// Reply format used by USERHOST to list replies to the query list.  The reply string is composed as follows:
        /// &lt;reply&gt; ::= &lt;nick&gt;['*'] '=' &lt;'+'|'-'&gt;&lt;hostname&gt;
        /// The '*' indicates whether the client has registered as an Operator.  The '-' or '+' characters represent whether the client has set an AWAY message or not respectively.
        /// </summary>
        /// <value></value>
        UserHost = 302,

        /// <summary>
        /// Reply format used by ISON to list replies to the query list.
        /// </summary>
        Ison = 303,

        /// <summary>
        /// User is now Away
        /// </summary>
        Away = 301,

        /// <summary>
        /// You are no longer marked as being away
        /// </summary>
        Unaway = 305,

        /// <summary>
        /// You are now marked as being away
        /// These replies are used with the AWAY command (if allowed).  RPL_AWAY is sent to any client sending a PRIVMSG to a client which is away.  RPL_AWAY is only sent by the server to which the client is connected. Replies RPL_UNAWAY and RPL_NOWAWAY are sent when the client removes and sets an AWAY message.
        /// </summary>
        NowAway = 306,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        WhoIsUser = 311,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        WhoIsServer = 312,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        WhoIsOperator = 313,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        WhoIsIdle = 317,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        EndOfWhoIs = 318,

        /// <summary>
        /// Replies 311 - 313, 317 - 319 are all replies generated in response to a WHOIS message.  Given that there are enough parameters present, the answering server must either formulate a reply out of the above numerics (if the query nick is found) or return an error reply.  The '*' in RPL_WHOISUSER is there as the literal character and not as a wild card.  For each reply set, only RPL_WHOISCHANNELS may appear more than once (for long lists of channel names). The '@' and '+' characters next to the channel name indicate whether a client is a channel operator or has been granted permission to speak on a moderated channel.  The RPL_ENDOFWHOIS reply is used to mark the end of processing a WHOIS message.
        /// </summary>
        WhoIsChannels = 318,

        /// <summary>
        /// When replying to a WHOWAS message, a server must use the replies RPL_WHOWASUSER, RPL_WHOISSERVER or ERR_WASNOSUCHNICK for each nickname in the presented list.  At the end of all reply batches, there must be RPL_ENDOFWHOWAS (even if there was only one reply and it was an error).
        /// </summary>
        WhoWasUser = 314,

        /// <summary>
        /// When replying to a WHOWAS message, a server must use the replies RPL_WHOWASUSER, RPL_WHOISSERVER or ERR_WASNOSUCHNICK for each nickname in the presented list.  At the end of all reply batches, there must be RPL_ENDOFWHOWAS (even if there was only one reply and it was an error).
        /// </summary>
        EndOfWhoWas = 369,

        /// <summary>
        /// Replies RPL_LISTSTART, RPL_LIST, RPL_LISTEND mark the start, actual replies with data and end of the server's response to a LIST command.  If there are no channels available to return, only the start and end reply must be sent.
        /// </summary>
        ListStart = 321,

        /// <summary>
        /// Replies RPL_LISTSTART, RPL_LIST, RPL_LISTEND mark the start, actual replies with data and end of the server's response to a LIST command.  If there are no channels available to return, only the start and end reply must be sent.
        /// </summary>
        List = 322,

        /// <summary>
        /// Replies RPL_LISTSTART, RPL_LIST, RPL_LISTEND mark the start, actual replies with data and end of the server's response to a LIST command.  If there are no channels available to return, only the start and end reply must be sent.
        /// </summary>
        ListEnd = 323,

        /*
        324     RPL_CHANNELMODEIS      

        331     RPL_NOTOPIC       "<channel> :No topic is set"
        332     RPL_TOPIC       "<channel> :<topic>"

        - When sending a TOPIC message to determine the channel topic, one of two replies is sent.  If the topic is set, RPL_TOPIC is sent back else RPL_NOTOPIC.

        341     RPL_INVITING       "<channel> <nick>"

        - Returned by the server to indicate that the attempted INVITE message was successful and is being passed onto the end client.

        342     RPL_SUMMONING       "<user> :Summoning user to IRC"

        - Returned by a server answering a SUMMON message to indicate that it is summoning that user.

        351     RPL_VERSION       "<version>.<debuglevel> <server> :<comments>"

        - Reply by the server showing its version details. The <version> is the version of the software being used (including any patchlevel revisions) and the <debuglevel> is used to indicate if the server is running in "debug mode". The "comments" field may contain any comments about the version or further version details.

        352     RPL_WHOREPLY       "<channel> <user> <host> <server> <nick> \        <H|G>[*][@|+] :<hopcount> <real name>"
        315     RPL_ENDOFWHO       "<name> :End of /WHO list"

        - The RPL_WHOREPLY and RPL_ENDOFWHO pair are used to answer a WHO message.  The RPL_WHOREPLY is only sent if there is an appropriate match to the WHO query.  If there is a list of parameters supplied with a WHO message, a RPL_ENDOFWHO must be sent after processing each list item with <name> being the item.

        353     RPL_NAMREPLY       "<channel> :[[@|+]<nick> [[@|+]<nick> [...]]]"
        366     RPL_ENDOFNAMES       "<channel> :End of /NAMES list"

        - To reply to a NAMES message, a reply pair consisting of RPL_NAMREPLY and RPL_ENDOFNAMES is sent by the server back to the client.  If there is no channel found as in the query, then only RPL_ENDOFNAMES is returned.  The exception to this is when a NAMES message is sent with no parameters and all visible channels and contents are sent back in a series of RPL_NAMEREPLY messages with a RPL_ENDOFNAMES to mark the end.

        364     RPL_LINKS       "<mask> <server> :<hopcount> <server info>"
        365     RPL_ENDOFLINKS       "<mask> :End of /LINKS list"

        - In replying to the LINKS message, a server must send replies back using the RPL_LINKS numeric and mark the end of the list using an RPL_ENDOFLINKS reply.

        367     RPL_BANLIST       "<channel> <banid>"
        368     RPL_ENDOFBANLIST  "<channel> :End of channel ban list"

        - When listing the active 'bans' for a given channel, a server is required to send the list back using the RPL_BANLIST and RPL_ENDOFBANLIST messages.  A separate RPL_BANLIST is sent for each active banid.  After the banids have been listed (or if none present) a RPL_ENDOFBANLIST must be sent.

        371     RPL_INFO       ":<string>"
        374     RPL_ENDOFINFO       ":End of /INFO list"

        - A server responding to an INFO message is required to send all its 'info' in a series of RPL_INFO messages with a RPL_ENDOFINFO reply to indicate the end of the replies.

        375     RPL_MOTDSTART       ":- <server> Message of the day - "
        372     RPL_MOTD       ":- <text>"
        376     RPL_ENDOFMOTD       ":End of /MOTD command"

        - When responding to the MOTD message and the MOTD file is found, the file is displayed line by line, with each line no longer than 80 characters, using RPL_MOTD format replies.  These should be surrounded by a RPL_MOTDSTART (before the RPL_MOTDs) and an RPL_ENDOFMOTD (after).

        381     RPL_YOUREOPER       ":You are now an IRC operator"

        - RPL_YOUREOPER is sent back to a client which has just successfully issued an OPER message and gained operator status.

        382     RPL_REHASHING       "<config file> :Rehashing"

        - If the REHASH option is used and an operator sends a REHASH message, an RPL_REHASHING is sent back to the operator.

        391     RPL_TIME  "<server> :<string showing server's local time>"

        - When replying to the TIME message, a server must send the reply using the RPL_TIME format above.  The string showing the time need only contain the correct day and time there.  There is no further requirement for the time string.

        392     RPL_USERSSTART       ":UserID   Terminal  Host"
        393     RPL_USERS       ":%-8s %-9s %-8s"
        394     RPL_ENDOFUSERS       ":End of users"
        395     RPL_NOUSERS       ":Nobody logged in"

        - If the USERS message is handled by a server, the replies RPL_USERSTART, RPL_USERS, RPL_ENDOFUSERS and RPL_NOUSERS are used.  RPL_USERSSTART must be sent first, following by either a sequence of RPL_USERS or a single RPL_NOUSER.  Following this is RPL_ENDOFUSERS.

        200     RPL_TRACELINK       "Link <version & debug level> <destination> \        <next server>"
        201     RPL_TRACECONNECTING       "Try. <class> <server>"
        202     RPL_TRACEHANDSHAKE       "H.S. <class> <server>"
        203     RPL_TRACEUNKNOWN       "???? <class> [<client IP address in dot form>]"
        204     RPL_TRACEOPERATOR       "Oper <class> <nick>"
        205     RPL_TRACEUSER       "User <class> <nick>"
        206     RPL_TRACESERVER       "Serv <class> <int>S <int>C <server> \        <nick!user|*!*>@<host|server>"
        208     RPL_TRACENEWTYPE       "<newtype> 0 <client name>"
        261     RPL_TRACELOG       "File <logfile> <debug level>"

        - The RPL_TRACE* are all returned by the server in response to the TRACE message.  How many are returned is dependent on the the TRACE message and whether it was sent by an operator or not.  There is no predefined order for which occurs first. Replies RPL_TRACEUNKNOWN, RPL_TRACECONNECTING and RPL_TRACEHANDSHAKE are all used for connections which have not been fully established and are either unknown, still attempting to connect or in the process of completing the 'server handshake'. RPL_TRACELINK is sent by any server which handles a TRACE message and has to pass it on to another server.  The list of RPL_TRACELINKs sent in response to a TRACE command traversing the IRC network should reflect the actual connectivity of the servers themselves along that path. RPL_TRACENEWTYPE is to be used for any connection which does not fit in the other categories but is being displayed anyway.

        211     RPL_STATSLINKINFO       "<linkname> <sendq> <sent messages> \        <sent bytes> <received messages> \        <received bytes> <time open>"
        212     RPL_STATSCOMMANDS       "<command> <count>"
        213     RPL_STATSCLINE       "C <host> * <name> <port> <class>"
        214     RPL_STATSNLINE       "N <host> * <name> <port> <class>"
        215     RPL_STATSILINE       "I <host> * <host> <port> <class>"
        216     RPL_STATSKLINE       "K <host> * <username> <port> <class>"
        218     RPL_STATSYLINE       "Y <class> <ping frequency> <connect \        frequency> <max sendq>"
        219     RPL_ENDOFSTATS       "<stats letter> :End of /STATS report"
        241     RPL_STATSLLINE       "L <hostmask> * <servername> <maxdepth>"
        242     RPL_STATSUPTIME       ":Server Up %d days %d:%02d:%02d"
        243     RPL_STATSOLINE       "O <hostmask> * <name>"
        244     RPL_STATSHLINE       "H <hostmask> * <servername>"

        221     RPL_UMODEIS       "<user mode string>"
        - To answer a query about a client's own mode,         RPL_UMODEIS is sent back.

        251     RPL_LUSERCLIENT       ":There are <integer> users and <integer> \        invisible on <integer> servers"
        252     RPL_LUSEROP       "<integer> :operator(s) online"
        253     RPL_LUSERUNKNOWN       "<integer> :unknown connection(s)"
        254     RPL_LUSERCHANNELS       "<integer> :channels formed"
        255     RPL_LUSERME       ":I have <integer> clients and <integer> \         servers"
        - In processing an LUSERS message, the server         sends a set of replies from RPL_LUSERCLIENT,         RPL_LUSEROP, RPL_USERUNKNOWN,         RPL_LUSERCHANNELS and RPL_LUSERME.  When         replying, a server must send back         RPL_LUSERCLIENT and RPL_LUSERME.  The other         replies are only sent back if a non-zero count         is found for them.

        256     RPL_ADMINME       "<server> :Administrative info"
        257     RPL_ADMINLOC1       ":<admin info>"
        258     RPL_ADMINLOC2       ":<admin info>"
        259     RPL_ADMINEMAIL       ":<admin info>"
        - When replying to an ADMIN message, a server         is expected to use replies RLP_ADMINME         through to RPL_ADMINEMAIL and provide a text         message with each.  For RPL_ADMINLOC1 a         description of what city, state and country         the server is in is expected, followed by         details of the university and department         (RPL_ADMINLOC2) and finally the administrative         contact for the server (an email address here         is required) in RPL_ADMINEMAIL.

        */
    }
}