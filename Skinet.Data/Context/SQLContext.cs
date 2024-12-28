using System;
using Microsoft.EntityFrameworkCore;

namespace Skinet.Data.Context;

public class SQLContext(DbContextOptions options) : StoreContext(options)
{
    //
}
