using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeRequestSubSystem.ControlClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChangeRequestSubSystem.ControlClasses.Tests
{

    [TestClass()]
    public abstract class AbstractModelTestCase
    {
        
        public AbstractModelTestCase()
        {
            CRSubSystemAPI.Initialize();
        }

        [TestInitialize]
        public virtual void Init()
        {
           
        }

  
        [TestCleanup]
        public virtual void Terminate()
        {
            
        }

        [ClassCleanup]
        public virtual void TerminateAll()
        {
        }

       
    }
}
