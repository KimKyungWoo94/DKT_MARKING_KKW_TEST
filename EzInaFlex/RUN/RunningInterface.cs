using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public interface Run_Interface
    {
        bool ToInit();
        void Init();

        bool ToReady();
        void Ready();

        bool ToRun(bool a_bPauseStop = true);
        bool Run();

        bool ToStop();
        void Stop();

        bool FindAction();

		    void ConnectingModule();
    }
}
