using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.IO
{
    public class Cylinder:NotifyProperyChangedBase
    {
        private DI m_DIForwardSensor;
        private DI m_DIBackwardSensor;
        private DO m_DOForwardSol;
        private DO m_DOBackwardSol;
        private string m_strID;
        private string m_strDesrc;
        private bool m_bForwardSersorCheck;
        private bool m_bBackwardSersorCheck;
        private int  m_iSolCheckDelay;
        private CylinderType m_OperationType;         
        private Stopwatch    m_SolDelayWatch;   
        private Thread       m_pExecuteThread;  
        private object       m_pThreadLock;
        private int          m_iRepeatCount;
        private int          m_iExecuteRepeatCount;
        private bool         m_bExecuteEnable;    
            
        public Cylinder(DI a_DIForwardSensor,
                        DI a_DIBackWordSensor,
                        DO a_DOForwardSol,
                        DO a_DOBackWordSol,
                        string a_strID, 
                        string a_strDesrc,
                        bool a_bForwardSersorCheck,
                        bool a_bBackwardSersorCheck,
                        int  a_iSolCheckDelay,
                        CylinderType a_Type )
        {
            m_DIForwardSensor = a_DIForwardSensor;
            m_DIBackwardSensor = a_DIBackWordSensor;
            m_DOForwardSol = a_DOForwardSol;
            m_DOBackwardSol = a_DOBackWordSol;
            m_strID = a_strID;
            m_strDesrc = a_strDesrc;
            m_bForwardSersorCheck = a_bForwardSersorCheck;
            m_bBackwardSersorCheck = a_bBackwardSersorCheck;
            m_iSolCheckDelay=a_iSolCheckDelay;
            m_OperationType = a_Type;   
            m_SolDelayWatch=new Stopwatch();  
            m_pThreadLock=new object();              
            m_iRepeatCount=10;                             
        }
        public string strID
        {
            get { return m_strID;}
        }
        public string strDesrc
        {
            get { return m_strDesrc;}
        }
        public bool bForwardSersorCheck
        {
            get { return m_bForwardSersorCheck;}
            set { base.CheckPropertyChanged<bool>("bForwardSersorCheck",ref m_bForwardSersorCheck ,value); }
        }
        public bool bBackwardSersorCheck
        {
            get { return m_bBackwardSersorCheck; }
            set { base.CheckPropertyChanged<bool>("bBackwardSersorCheck",ref m_bBackwardSersorCheck ,value); }
        }
        public CylinderType OperType
        {
            get { return m_OperationType;}
            set { base.CheckPropertyChanged<CylinderType>("OperType",ref m_OperationType ,value);}
        }
        public int SolCheckDelay
        {
            get { return m_iSolCheckDelay;}
            set {base.CheckPropertyChanged<int>("OperType",ref m_iSolCheckDelay ,value); }
        }
        public int RepeatCount
        {
            get { return m_iRepeatCount;}
            set {base.CheckPropertyChanged<int>("RepeatCount",ref m_iRepeatCount ,value);  }
        }
        public int ExecuteRepeatCount
        {
            get {return m_iExecuteRepeatCount; }
        }


        public DI ForwardSensor
        {
            get {return m_DIForwardSensor; }
            set
            {
                if(value!=null)
                  m_DIForwardSensor=null;
            }
        } 
        public DI BackwardSensor
        {
            get { return m_DIBackwardSensor;}
            set
            {
                if(value!=null)
                 m_DIBackwardSensor=value;
            }
        } 
        public DO ForwardSol
        {
            get {return m_DOForwardSol; }
            set
            {
               if(value!=null)
                m_DOForwardSol=value;
            }
        } 
        public DO BackwardSol
        {
            get { return m_DOBackwardSol;}
            set
            {
                if(value!=null)
                m_DOBackwardSol =value;
            }
        }                
        
        public EContact EContactForwardSensor
        {
            get {  return m_DIForwardSensor!=null ? m_DIForwardSensor.EContact:EContact.A;}
            set 
            {
                if(m_DIForwardSensor!=null)
                {
                    m_DIForwardSensor.EContact=value;
                }
            }
        }
        public EContact EContactBackwardSensor
        {
            get { return m_DIBackwardSensor != null ? m_DIBackwardSensor.EContact : EContact.A; }
            set
            {
                if (m_DIBackwardSensor != null)
                {
                    m_DIBackwardSensor.EContact = value;
                }
            } 
        }
        public CylinderState CurrentState
        {
            get
            {
                if (m_SolDelayWatch.IsRunning)
                {
                    if (m_SolDelayWatch.ElapsedMilliseconds >= m_iSolCheckDelay)
                    {
                       return InteralCylinderStatus();
                    }
                    else
                    {
                        return CylinderState.UNKNOWN;
                    }
                }
                else
                {
                    return InteralCylinderStatus();
                }               
            }
        }
        private  CylinderState InteralCylinderStatus()
        {
            CylinderState Ret=CylinderState.UNKNOWN;
            if(m_OperationType==CylinderType.SINGLE)
            {
                Ret=InternalSingleActionSensorCheck();
            }
            else
            {
                Ret=InternalDoubleActionSensorCheck();                     
            }
           return Ret;
        }
        private CylinderState InternalSingleActionSensorCheck()
        {
            if (m_bForwardSersorCheck)
            {
                if (m_DIForwardSensor == null)
                    return CylinderState.UNKNOWN;
                if (m_DIForwardSensor.Value == true)
                {
                    return CylinderState.FORWARD;
                }
            }
            else
            {
                if (m_DOForwardSol == null)
                    return CylinderState.UNKNOWN;
                if (m_DOForwardSol.Value == true)
                {
                     return CylinderState.FORWARD;
                }
                else
                {
                     return CylinderState.BACKWARD;
                }               
            }         
            return CylinderState.UNKNOWN;
        }
        private CylinderState InternalDoubleActionSensorCheck()
        {
            if (m_bForwardSersorCheck == true && m_bBackwardSersorCheck == true)
            {
                if(m_DIForwardSensor==null || m_DIBackwardSensor==null )
                 return CylinderState.UNKNOWN; 
                if (m_DIForwardSensor.Value == true && m_DIBackwardSensor.Value == false)
                {
                    return CylinderState.FORWARD;
                }
                else if (m_DIForwardSensor.Value == false && m_DIBackwardSensor.Value == true)
                {
                    return CylinderState.BACKWARD;
                }
            }
            else if (m_bForwardSersorCheck == true && m_bBackwardSersorCheck == false)
            {
                if (m_DIForwardSensor == null )
                    return CylinderState.UNKNOWN;

                if (m_DIForwardSensor.Value == true)
                {
                    return CylinderState.FORWARD;
                }
                else
                {
                    return CylinderState.BACKWARD;
                }
            }
            else if (m_bForwardSersorCheck == false && m_bBackwardSersorCheck == true)
            {
                if (m_DIBackwardSensor == null)
                    return CylinderState.UNKNOWN;
                if (m_DIBackwardSensor.Value == true)
                {
                    return CylinderState.BACKWARD;
                }
                else
                {
                    return CylinderState.FORWARD;
                }
            }  
            else
            {
                if (m_DOForwardSol == null || m_DOBackwardSol == null)
                    return CylinderState.UNKNOWN;
                if (m_DOForwardSol.Value == true && m_DOBackwardSol.Value == false)
                {
                    return CylinderState.FORWARD;
                }
                else if (m_DOForwardSol.Value == false && m_DOBackwardSol.Value == true)
                {
                    return CylinderState.BACKWARD;
                }
            }         
           return CylinderState.UNKNOWN;
        }

        public bool Action(bool a_value)
        {
            if(m_bExecuteEnable==false)
            {
                InternalAction(a_value);
                return true;
            }
            return false;
        }
        public bool StartRepeatAction()
        {
            if (m_bExecuteEnable == false)
            {
                m_bExecuteEnable = true;
                m_SolDelayWatch.Stop();
                m_SolDelayWatch.Reset();
                m_pExecuteThread=new Thread(new ParameterizedThreadStart(Execute));
                m_pExecuteThread.IsBackground=true;
                m_pExecuteThread.Start(new object[] { m_iRepeatCount,m_iSolCheckDelay });
            }
            return false;
        }
        public void StopRepeatAction()
        {
            m_bExecuteEnable=false;
            m_SolDelayWatch.Stop();
            m_SolDelayWatch.Reset();
        }
        public bool IsRepeatActionExcute
        {
            get { return m_bExecuteEnable | m_pExecuteThread!=null?m_pExecuteThread.IsAlive:false;}
        }
        private void InternalAction(bool a_value)
        {
            switch(m_OperationType)
            {
                case CylinderType.SINGLE:
                    {                        

                        this.m_DOForwardSol.Value=a_value;
                        m_SolDelayWatch.Restart();
#if SIM
                            if (this.m_bForwardSersorCheck)
                            {
                                this.m_DIForwardSensor.OrginValue = a_value==true ?a_value : !a_value;
                            }
                            else
                            {
                                this.m_DIForwardSensor.OrginValue =  a_value==true?!a_value:a_value;
                            }                                        
                         
#endif
                    }
                    break;
                case CylinderType.DOUBLE:
                    {
                        this.m_DOForwardSol.Value=a_value;
                        this.m_DOBackwardSol.Value=!a_value;
                        m_SolDelayWatch.Restart();
#if SIM
                           if (this.m_bForwardSersorCheck)
                            {
                                this.m_DIForwardSensor.OrginValue = a_value;
                            }
                            else
                            {
                                this.m_DIForwardSensor.OrginValue = this.m_DOForwardSol.Value ;
                            }              

                            if (this.m_bBackwardSersorCheck)
                            {
                                this.m_DIBackwardSensor.OrginValue = !a_value;
                            }
                            else
                            {
                                this.m_DIBackwardSensor.OrginValue =  this.m_DOBackwardSol.Value;
                            }                                
#endif

                    }
                    break;
            }
        }
        private void Execute(object Params)
        {
            object [] ParamList=(object [])Params;        
            if (ParamList.Length>0)
            {
                m_iExecuteRepeatCount=0;
                int iMaxRepeatCount = (int)ParamList[0];
                int iSolCheckDelay=(int)ParamList[1];   
                bool bFirstExecute=false;             
                lock (m_pThreadLock)
                {
                    while (m_bExecuteEnable)
                    {
                        if(m_SolDelayWatch.IsRunning==false)
                        {
                            Trace.WriteLine(string.Format("{0} : {1}",CurrentState,m_iRepeatCount));
                            switch (CurrentState)
                            {
                                case CylinderState.FORWARD:
                                    {
                                        InternalAction(false);
                                    }
                                    break;
                                case CylinderState.BACKWARD:
                                    {
                                        InternalAction(true);
                                    }
                                    break;
                                case CylinderState.UNKNOWN:
                                    {
                                        if(bFirstExecute==false)
                                        {
                                            InternalAction(false);
                                            bFirstExecute=true;
                                        }                                        
                                    }
                                    break;
                            }
                        }                      
                        else if (m_SolDelayWatch.IsRunning && m_SolDelayWatch.ElapsedMilliseconds >= iSolCheckDelay)
                        {
                            if (iMaxRepeatCount > 0)
                            {
                                if(CurrentState!=CylinderState.UNKNOWN)
                                {
                                     m_iExecuteRepeatCount++;
                                     m_SolDelayWatch.Stop();
                                     m_SolDelayWatch.Reset();
                                     if (m_iExecuteRepeatCount >= iMaxRepeatCount)
                                     {
                                         break;
                                     }
                                     Thread.Sleep(500);   
                                }                             
                            }
                        }                                             
                    }
                }       
                m_SolDelayWatch.Stop();
                m_SolDelayWatch.Reset();
                m_bExecuteEnable =false;               
            }
        }   
        public CylinderIOData CreateIOData()
        {          
            return new CylinderIOData(m_DIForwardSensor!=null   ?   m_DIForwardSensor.ID    : "",
                                      m_DIBackwardSensor!=null  ?   m_DIBackwardSensor.ID   : "",
                                      m_DOForwardSol!=null      ?   m_DOForwardSol.ID       : "",
                                      m_DOBackwardSol!=null     ?   m_DOBackwardSol.ID      : "",
                                      m_strID,
                                      m_strDesrc,
                                      m_bForwardSersorCheck,
                                      m_bBackwardSersorCheck,
                                      m_iSolCheckDelay,
                                      m_OperationType                                   
                                      );
        }       
    }
}
