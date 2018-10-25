using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevArt
{
    public interface IFile
    {
         FileModel Read();
         void Save(FileModel file); 
        
    }
}
