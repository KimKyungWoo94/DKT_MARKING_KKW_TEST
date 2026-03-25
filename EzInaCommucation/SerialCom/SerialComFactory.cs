using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace EzIna.Commucation.SerialCom
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SerialComFactory : SingleTone<SerialComFactory>,IDisposable
    {
        Dictionary<string,SerialPort> m_pSerialList;
        CSerialPortWatcher            m_pSerialPortWatcher;
        /// <summary>
        /// 
        /// </summary>
        protected override void OnCreateInstance()
        {
            m_pSerialList=new Dictionary<string, SerialPort>();
            m_pSerialPortWatcher=new CSerialPortWatcher();
            InitializePort();         
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
           foreach (KeyValuePair<string,SerialPort> item in m_pSerialList)
           {
                if(item.Value.IsOpen)
                {
                    item.Value.Close();
                    item.Value.Dispose();
                }
           }
        }    
        /// <summary>
        /// 
        /// </summary>                    
        private void InitializePort()
        {
             IEnumerable<string> ComPorts=SerialPort.GetPortNames().OrderBy(s => s);            
             foreach ( string Comport in ComPorts)
             {
                m_pSerialList.Add(Comport,new SerialPort(Comport));
             }
        }    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>                    
        private void OnRemovePortDetected(object sender, SerialPortChangeInfoEventArgs e)
        {
            foreach(string Port in e.PortList)
            {
                if(m_pSerialList.ContainsKey(Port))
                {
                    if(m_pSerialList[Port].IsOpen)
                    {
                        m_pSerialList[Port].Close();
                        m_pSerialList[Port].Dispose();
                    }
                    m_pSerialList.Remove(Port);
                }                
            }
        }
        private void OnAddNewPortDetected(object sender, SerialPortChangeInfoEventArgs e)
        {
            foreach (string Port in e.PortList)
            {
                if (!m_pSerialList.ContainsKey(Port))
                {                   
                    m_pSerialList.Add(Port,new SerialPort(Port));
                }
            }
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPortName"></param>
        /// <returns></returns>
        public bool IsExistPort(string strPortName)
        {
            return m_pSerialList.ContainsKey(strPortName);
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strComport"></param>
        /// <returns></returns>                    
        public SerialPort this[string strComport]
        {
            get
            {
                if(m_pSerialList.ContainsKey(strComport))
                    return m_pSerialList[strComport];
                return null;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public sealed class CSerialPortWatcher : IDisposable
    {

        event SerialPortChangeHandler m_RemoveComPortEvent;
        /// <summary>
        /// 
        /// </summary>
        public event SerialPortChangeHandler RemoveComPortEvent
        {
           add
           {
                m_RemoveComPortEvent+=value;
           }
           remove
           {
                m_RemoveComPortEvent-=value;
           }
        }
        event SerialPortChangeHandler m_AddNewComPortEvent;
        event SerialPortChangeHandler AddNewComPortEvent
        {
           add
           {
                m_AddNewComPortEvent+=value;
           }
           remove
           {
                m_AddNewComPortEvent-=value;
           }
        }
        /// <summary>
        /// 
        /// </summary>
        public CSerialPortWatcher()
        {
            _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            ComPorts = new ObservableCollection<string>(SerialPort.GetPortNames().OrderBy(s => s));
            RemoveComPorts=new ObservableCollection<string>();
            AddComPorts=new ObservableCollection<string>();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent");

            _watcher = new ManagementEventWatcher(query);
            _watcher.EventArrived += (sender, eventArgs) => CheckForNewPorts(eventArgs);
            _watcher.Start();
        }

        private void CheckForNewPorts(EventArrivedEventArgs args)
        {
            // do it async so it is performed in the UI thread if this class has been created in the UI thread
            Task.Factory.StartNew(CheckForNewPortsAsync, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
        }

        private void CheckForNewPortsAsync()
        {
            IEnumerable<string> ports = SerialPort.GetPortNames().OrderBy(s => s);
            RemoveComPorts.Clear();
            AddComPorts.Clear();
            foreach (string comPort in ComPorts)
            {
                if (!ports.Contains(comPort))
                {
                    ComPorts.Remove(comPort);
                    RemoveComPorts.Add(comPort);
                }
                if(ComPorts.Count<=0)
                {
                    break;
                }
            }

            foreach (var port in ports)
            {
                if (!ComPorts.Contains(port))
                {
                    AddPort(port);
                    AddComPorts.Add(port);
                }
            }

            if(RemoveComPorts.Count>0)
            {
                RemoveComPorts.OrderBy(s=>s);
                 m_RemoveComPortEvent(this,new SerialPortChangeInfoEventArgs(RemoveComPorts.ToList()));
            }
               
            if(AddComPorts.Count>0)
            {
                AddComPorts.OrderBy(s=>s);
                m_AddNewComPortEvent(this,new SerialPortChangeInfoEventArgs(AddComPorts.ToList()));
            }
                
            ComPorts.OrderBy(s => s); 
        }
        private void AddPort(string port)
        {
            if (!ComPorts.Contains(port))
            {
                ComPorts.Add(port);     
                         
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> ComPorts { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> RemoveComPorts { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> AddComPorts { get; private set; }
        #region IDisposable Members
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _watcher.Stop();
        }

        #endregion
        private ManagementEventWatcher _watcher;
        private TaskScheduler _taskScheduler;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SerialPortChangeHandler(object sender, SerialPortChangeInfoEventArgs e);
    /// <summary>
    /// 
    /// </summary>
    public class SerialPortChangeInfoEventArgs : EventArgs
    {
        private List<string> m_PortList;
        /// <summary>
        /// 
        /// </summary>
        public  List<string>  PortList
        {
            get { return m_PortList; }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_PortList"></param>
        public SerialPortChangeInfoEventArgs(List<string>a_PortList)
        {
            m_PortList=new List<string>(a_PortList);            
        }
    }

}
