using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using PubnubApi;

public class cpubnub
{
    public cpubnub()
    {
        PNConfiguration pnConfiguration = new PNConfiguration(new UserId("irwan.agusyono@gmail.com"));
        pnConfiguration.SubscribeKey = "sub-c-27aab402-3c50-11e9-bbfc-8200a0d642df";
        pnConfiguration.PublishKey = "pub-c-07c8f1e7-973f-4dcf-b70f-99700208b764";
        Pubnub pubnub = new Pubnub(pnConfiguration);

        // Adding listener.
        pubnub.AddListener(new SubscribeCallbackExt(
            delegate (Pubnub pnObj, PNMessageResult<object> pubMsg)
            {
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(pubMsg));
                var channelName = pubMsg.Channel;
                var channelGroupName = pubMsg.Subscription;
                var pubTT = pubMsg.Timetoken;
                var msg = pubMsg.Message;
                var publisher = pubMsg.Publisher;
            },
            delegate (Pubnub pnObj, PNPresenceEventResult presenceEvnt)
            {
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(presenceEvnt));
                var action = presenceEvnt.Event; // Can be join, leave, state-change or timeout
                var channelName = presenceEvnt.Channel; // The channel for which the message belongs
                var occupancy = presenceEvnt.Occupancy; // No. of users connected with the channel
                var state = presenceEvnt.State; // User State
                var channelGroupName = presenceEvnt.Subscription; //  The channel group or wildcard subscription match (if exists)
                var publishTime = presenceEvnt.Timestamp; // Publish timetoken
                var timetoken = presenceEvnt.Timetoken;  // Current timetoken
                var uuid = presenceEvnt.Uuid; // UUIDs of users who are connected with the channel
            },
            delegate (Pubnub pnObj, PNSignalResult<object> signalMsg)
            {
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(signalMsg));
                var channelName = signalMsg.Channel; // The channel for which the signal belongs
                var channelGroupName = signalMsg.Subscription; // The channel group or wildcard subscription match (if exists)
                var pubTT = signalMsg.Timetoken; // Publish timetoken
                var msg = signalMsg.Message; // The Payload
                var publisher = signalMsg.Publisher; //The Publisher
            },
            delegate (Pubnub pnObj, PNObjectEventResult objectEventObj)
            {
                var channelName = objectEventObj.Channel; // Channel
                var channelMetadata = objectEventObj.ChannelMetadata; //Channel Metadata
                var uidMetadata = objectEventObj.UuidMetadata; // UUID metadata
                var evnt = objectEventObj.Event; // Event
                var type = objectEventObj.Type; // Event type
                if (objectEventObj.Type == "uuid")
                {
                    /* got uuid metadata related event. */
                }
                else if (objectEventObj.Type == "channel")
                {
                    /* got channel metadata related event. */
                }
                else if (objectEventObj.Type == "membership")
                {
                    /* got membership related event. */
                }
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(objectEventObj));
            },
            delegate (Pubnub pnObj, PNMessageActionEventResult msgActionEvent)
            {
                //handle message action
                var channelName = msgActionEvent.Channel; // The channel for which the message belongs
                var msgEvent = msgActionEvent.Action; // message action added or removed
                var msgActionType = msgActionEvent.Event; // message action type
                var messageTimetoken = msgActionEvent.MessageTimetoken; // The timetoken of the original message
                var actionTimetoken = msgActionEvent.ActionTimetoken; //The timetoken of the message action
            },
            delegate (Pubnub pnObj, PNFileEventResult fileEvent)
            {
                //handle file message event
                var channelName = fileEvent.Channel;
                var chanelGroupName = fileEvent.Subscription;
                var fieldId = (fileEvent.File != null) ? fileEvent.File.Id : null;
                var fileName = (fileEvent.File != null) ? fileEvent.File.Name : null;
                var fileUrl = (fileEvent.File != null) ? fileEvent.File.Url : null;
                var fileMessage = fileEvent.Message;
                var filePublisher = fileEvent.Publisher;
                var filePubTT = fileEvent.Timetoken;
            },
            delegate (Pubnub pnObj, PNStatus pnStatus)
            {
                Console.WriteLine("{0} {1} {2}", pnStatus.Operation, pnStatus.Category, pnStatus.StatusCode);
                var affectedChannelGroups = pnStatus.AffectedChannelGroups; // The channel groups affected in the operation, of type array.
                var affectedChannels = pnStatus.AffectedChannels; // The channels affected in the operation, of type array.
                var category = pnStatus.Category; //Returns PNConnectedCategory
                var operation = pnStatus.Operation; //Returns PNSubscribeOperation
            }
        ));

        //Add listener to receive Signal messages
        SubscribeCallbackExt signalSubscribeCallback = new SubscribeCallbackExt(
            delegate (Pubnub pubnubObj, PNSignalResult<object> message) {
                // Handle new signal message stored in message.Message
            },
            delegate (Pubnub pubnubObj, PNStatus status)
            {
                // the status object returned is always related to subscribe but could contain
                // information about subscribe, heartbeat, or errors
            }
        );
        pubnub.AddListener(signalSubscribeCallback);

        SubscribeCallbackExt eventListener = new SubscribeCallbackExt(delegate (Pubnub pnObj, PNObjectEventResult objectEvent)
    {
        string channelMetadataId = objectEvent.Channel; // The channel
        //string uuidMetadataId = objectEvent.Uuid; // The UUID
        string objEvent = objectEvent.Event; // The event name that occurred
        string eventType = objectEvent.Type; // The event type that occurred
        PNUuidMetadataResult uuidMetadata = objectEvent.UuidMetadata; // UuidMetadata
        PNChannelMetadataResult channelMetadata = objectEvent.ChannelMetadata; // ChannelMetadata
    },
    delegate (Pubnub pnObj, PNStatus status)
    {

    }
);
        pubnub.AddListener(eventListener);

    }
}