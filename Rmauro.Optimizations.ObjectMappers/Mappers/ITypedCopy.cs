using System;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public interface ITypedCopy
{
    void Copy(ref RandomModel source, ref RandomModel target);
}
