using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace RDSSNLSMPUtilsClasses
{
    public class cMsgQ
    {
        public string MessageQueueName { get; set; }

        public cMsgQ(string QueName)
        {
            this.MessageQueueName = QueName;
        }

        public bool AddMessage(string Message)
        {

            try
            {
                cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);
                var msg = new Message();
                msg.UseDeadLetterQueue = true;
                msg.UseJournalQueue = true;
                msg.AcknowledgeType = AcknowledgeTypes.FullReachQueue | AcknowledgeTypes.FullReceive;

                msg.Body = Message;
                msg.Label = "Payment " + DateTime.Today.Month + ":" + DateTime.Today.Day + ":" + DateTime.Today.Year + ":" + DateTime.Today.Hour + DateTime.Today.Minute + DateTime.Today.Second + DateTime.Today.Millisecond;

                msg.AdministrationQueue = new MessageQueue(@".\private$\Ack");

                var mq = new MessageQueue
                    (@"FormatName:DIRECT=OS: " + oSettings.PayQuePath);

                mq.Send(msg);

                return true;
            }
            catch (MessageQueueException mqe)
            {
                throw mqe;
            }
        }

    }
}
