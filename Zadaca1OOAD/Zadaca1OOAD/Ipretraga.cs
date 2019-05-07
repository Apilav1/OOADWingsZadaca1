using System;
using System.Collections.Generic;
using System.Text;

namespace OOADWings
{
    interface Ipretraga
    {
        IAvion PretragaPrekoJibAviona(string JibA);
        List<IAvion> PretragaPoAtributima(IAvion putnickiAvion);
    }
}
