using System;
using Microsoft.EntityFrameworkCore;

namespace Skinet.Data.Context;

public class PostgresContext(DbContextOptions options) : StoreContext(options)
{
    //
}
