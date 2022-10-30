using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.Data.MySqlClient;

#nullable disable

namespace AccesoDatos
{
    public partial class DataBaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataBaseContext()
        {
        }
        public string conexionString = "";
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asiento> Asientos { get; set; }
        public virtual DbSet<Boleto> Boletos { get; set; }
        public virtual DbSet<BoletosFactura> BoletosFacturas { get; set; }
        public virtual DbSet<ClasificacionPelicula> ClasificacionPeliculas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Funcione> Funciones { get; set; }
        public virtual DbSet<MetodosPago> MetodosPagos { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Pelicula> Peliculas { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesUsuario> RolesUsuarios { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }
        public virtual DbSet<Sucursale> Sucursales { get; set; }
        public virtual DbSet<SucursalesUsuario> SucursalesUsuarios { get; set; }
        public virtual DbSet<TipoBoleto> TipoBoletos { get; set; }
        public virtual DbSet<TiposAsiento> TiposAsientos { get; set; }
        public virtual DbSet<TiposSala> TiposSalas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<ImagenPelicula> ImagenesPelicula { get; set; }
        public virtual DbSet<ValorEntero> valorEntero { get; set; }
        public virtual DbSet<ValorString> valorString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.Write);
            optionsBuilder.EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
                MySqlConnectionStringBuilder conn = ConectionStringBuilder();
                conexionString = conn.ConnectionString;
                string str = conn.ConnectionString;
                optionsBuilder.UseMySQL(str);
            }
        }

        public static MySqlConnectionStringBuilder ConectionStringBuilder()
        {
            var connectionString = new MySqlConnectionStringBuilder()
            {
                SslMode = MySqlSslMode.None,
                Server = "/cloudsql/fluent-observer-362922:us-central1:db-proyecto-analisis2",
                UserID = "Fernando",
                Password = "ferluan123",
                Database = "db_cinema",
                ConnectionProtocol = MySqlConnectionProtocol.UnixSocket
            };
            connectionString.Pooling = true;
            return connectionString;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ValorEntero>(entity =>
            {
                entity.HasNoKey();
                
                entity.Property(e => e.valor).HasColumnName("valor");

                
            });

            modelBuilder.Entity<ValorString>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.valor).HasColumnName("valor");


            });

            modelBuilder.Entity<Asiento>(entity =>
            {
                entity.HasKey(e => e.CodAsiento)
                    .HasName("PRIMARY");

                entity.ToTable("asientos");

                entity.HasIndex(e => e.CodSala, "FK_SALA_ASIENTO_idx");

                entity.HasIndex(e => e.CodTipoAsiento, "FK_TIPO_ASIENTO_idx");

                entity.Property(e => e.CodAsiento).HasColumnName("cod_asiento");

                entity.Property(e => e.CodSala).HasColumnName("cod_sala");

                entity.Property(e => e.CodTipoAsiento).HasColumnName("cod_tipo_asiento");

                entity.Property(e => e.FechaAct)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("fecha_ing");

                entity.Property(e => e.Fila)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("fila")
                    .HasComment("A - Z");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("numero");

                entity.Property(e => e.UsuarioAct).HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng).HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodSalaNavigation)
                    .WithMany(p => p.Asientos)
                    .HasForeignKey(d => d.CodSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SALA_ASIENTO");

                entity.HasOne(d => d.CodTipoAsientoNavigation)
                    .WithMany(p => p.Asientos)
                    .HasForeignKey(d => d.CodTipoAsiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_ASIENTO");
            });

            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.HasKey(e => e.CodBoleto)
                    .HasName("PRIMARY");

                entity.ToTable("boletos");

                entity.HasIndex(e => e.CodAsiento, "FK_ASIENTO_BOLETO_idx");

                entity.HasIndex(e => e.CodFuncion, "FK_FUNCION_BOLETO_idx");

                entity.HasIndex(e => e.CodTipoBoleto, "FK_TIPO_BOLETO_idx");

                entity.Property(e => e.CodBoleto).HasColumnName("cod_boleto");

                entity.Property(e => e.CodAsiento).HasColumnName("cod_asiento");

                entity.Property(e => e.CodFuncion).HasColumnName("cod_funcion");

                entity.Property(e => e.CodTipoBoleto).HasColumnName("cod_tipo_boleto");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'V'")
                    .HasComment("V = vigente\\nN = vigente\\n");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodAsientoNavigation)
                    .WithMany(p => p.Boletos)
                    .HasForeignKey(d => d.CodAsiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASIENTO_BOLETO");

                entity.HasOne(d => d.CodFuncionNavigation)
                    .WithMany(p => p.Boletos)
                    .HasForeignKey(d => d.CodFuncion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FUNCION_BOLETO");

                entity.HasOne(d => d.CodTipoBoletoNavigation)
                    .WithMany(p => p.Boletos)
                    .HasForeignKey(d => d.CodTipoBoleto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_BOLETO");
            });

            modelBuilder.Entity<BoletosFactura>(entity =>
            {
                entity.HasKey(e => e.CodBoletoFactura)
                    .HasName("PRIMARY");

                entity.ToTable("boletos_factura");

                entity.HasIndex(e => e.CodBoleto, "FK_COD_BOLETO_BOLETO_idx");

                entity.HasIndex(e => e.CodFactura, "FK_COD_FACTURA_BOLETOS_idx");

                entity.Property(e => e.CodBoletoFactura).HasColumnName("cod_boleto_factura");

                entity.Property(e => e.CodBoleto).HasColumnName("cod_boleto");

                entity.Property(e => e.CodFactura).HasColumnName("cod_factura");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodBoletoNavigation)
                    .WithMany(p => p.BoletosFacturas)
                    .HasForeignKey(d => d.CodBoleto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_BOLETO_BOLETOS");

                entity.HasOne(d => d.CodFacturaNavigation)
                    .WithMany(p => p.BoletosFacturas)
                    .HasForeignKey(d => d.CodFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_FACTURA_BOLETOS");
            });

            modelBuilder.Entity<ClasificacionPelicula>(entity =>
            {
                entity.HasKey(e => e.CodClasificacion)
                    .HasName("PRIMARY");

                entity.ToTable("clasificacion_peliculas");

                entity.Property(e => e.CodClasificacion).HasColumnName("cod_clasificacion");

                entity.Property(e => e.Activa)
                    .HasMaxLength(1)
                    .HasColumnName("activa")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.CodCliente).HasColumnName("cod_cliente");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nit)
                    .HasMaxLength(30)
                    .HasColumnName("nit");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombres");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("password");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(45)
                    .HasColumnName("telefono");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.CodDepartamento)
                    .HasName("PRIMARY");

                entity.ToTable("departamentos");

                entity.Property(e => e.CodDepartamento).HasColumnName("cod_departamento");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.CodFactura)
                    .HasName("PRIMARY");

                entity.ToTable("facturas");

                entity.HasIndex(e => e.CodCliente, "FK_COD_CLIENTE_FACTURA_idx");

                entity.HasIndex(e => e.CodMetodoPago, "FK_METODO_PAGO_FACTURA_idx");

                entity.Property(e => e.CodFactura).HasColumnName("cod_factura");

                entity.Property(e => e.CodCliente).HasColumnName("cod_cliente");

                entity.Property(e => e.CodMetodoPago).HasColumnName("cod_metodo_pago");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'P'")
                    .HasComment("P = pagada\nC = cancelada\n");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Iva).HasColumnName("iva");

                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.IdPromocion).HasColumnName("id_promocion");
                entity.Property(e => e.CantidadPromociones).HasColumnName("cantidad_promociones");
                entity.Property(e => e.DescuentoPromociones).HasColumnName("descuento_promocion");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_CLIENTE_FACTURA");

                entity.HasOne(d => d.CodMetodoPagoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CodMetodoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_METODO_PAGO_FACTURA");

                entity.HasOne(d => d.CodPromocionNavigation)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPromocion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROMOCION_FACTURA");

            });

            modelBuilder.Entity<Funcione>(entity =>
            {
                entity.HasKey(e => e.CodFuncion)
                    .HasName("PRIMARY");

                entity.ToTable("funciones");

                entity.HasIndex(e => e.CodSala, "FK_SALA_FUNCION_idx");

                entity.Property(e => e.CodFuncion).HasColumnName("cod_funcion");

                entity.Property(e => e.CodPelicula).HasColumnName("cod_pelicula");

                entity.Property(e => e.CodSala).HasColumnName("cod_sala");

                entity.Property(e => e.Estado)
                    .HasMaxLength(45)
                    .HasColumnName("estado")
                    .HasComment("P = pendiente\nC= cancelada\nT = terminada");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaHoraFin).HasColumnName("fecha_hora_fin");

                entity.Property(e => e.FechaHoraInicio).HasColumnName("fecha_hora_inicio");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodSalaNavigation)
                    .WithMany(p => p.Funciones)
                    .HasForeignKey(d => d.CodSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SALA_FUNCION");

                entity.HasOne(d => d.CodPeliculaNavigator)
                  .WithMany(p => p.Funciones)
                  .HasForeignKey(d => d.CodPelicula)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_PELICULA_FUNCION");
            });

            modelBuilder.Entity<MetodosPago>(entity =>
            {
                entity.HasKey(e => e.CodMetodoPago)
                    .HasName("PRIMARY");

                entity.ToTable("metodos_pago");

                entity.Property(e => e.CodMetodoPago).HasColumnName("cod_metodo_pago");

                entity.Property(e => e.Activo)
                    .HasMaxLength(1)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.CodMunicipio)
                    .HasName("PRIMARY");

                entity.ToTable("municipios");

                entity.HasIndex(e => e.CodDepartamento, "FK_COD_DEP_idx");

                entity.Property(e => e.CodMunicipio).HasColumnName("cod_municipio");

                entity.Property(e => e.CodDepartamento).HasColumnName("cod_departamento");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodDepartamentoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.CodDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_DEP");
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.CodPelicula)
                    .HasName("PRIMARY");

                entity.ToTable("peliculas");

                entity.HasIndex(e => e.CodClasificacion, "FK_CLASIF_PELI_idx");

                entity.Property(e => e.CodPelicula).HasColumnName("cod_pelicula");

                entity.Property(e => e.Activa)
                    .HasMaxLength(1)
                    .HasColumnName("activa")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.CodClasificacion).HasColumnName("cod_clasificacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Horas)
                    .HasColumnName("horas")
                    .HasComment("Cantidad de horas de duracion de la pelicula(Completas)");

                entity.Property(e => e.Minutos)
                    .HasColumnName("minutos")
                    .HasComment("Minutos restantes de las horas, ejemplo 2:20 de duracion, seria unicamente 20");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodClasificacionNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.CodClasificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLASIF_PELI");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.ColRol)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.ColRol).HasColumnName("col_rol");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'A'")
                    .HasComment("A = Activo\nN = No activo");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nombre_rol");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<RolesUsuario>(entity =>
            {
                entity.HasKey(e => new { e.CodUsuario, e.CodRol })
                    .HasName("PRIMARY");

                entity.ToTable("roles_usuario");

                entity.HasIndex(e => e.CodRol, "FK_COD_ROL_ROLES_idx");

                entity.Property(e => e.CodUsuario).HasColumnName("cod_usuario");

                entity.Property(e => e.CodRol).HasColumnName("cod_rol");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodRolNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.CodRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_ROL_ROLES");

                entity.HasOne(d => d.CodUsuarioNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.CodUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_USUARIO_ROLES");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.CodSala)
                    .HasName("PRIMARY");

                entity.ToTable("salas");

                entity.HasIndex(e => e.CodSucursal, "FK_SUCURSAL_SALA_idx");

                entity.HasIndex(e => e.CodTipoSala, "FK_TIPO_SALA_idx");

                entity.Property(e => e.CodSala).HasColumnName("cod_sala");

                entity.Property(e => e.Activa)
                    .HasMaxLength(1)
                    .HasColumnName("activa")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.CodSucursal).HasColumnName("cod_sucursal");

                entity.Property(e => e.CodTipoSala).HasColumnName("cod_tipo_sala");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'V'")
                    .HasComment("V = Vacia\nE = en uso");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodSucursalNavigation)
                    .WithMany(p => p.Salas)
                    .HasForeignKey(d => d.CodSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUCURSAL_SALA");

                entity.HasOne(d => d.CodTipoSalaNavigation)
                    .WithMany(p => p.Salas)
                    .HasForeignKey(d => d.CodTipoSala)
                    .HasConstraintName("FK_TIPO_SALA");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.HasKey(e => e.CodSucursal)
                    .HasName("PRIMARY");

                entity.ToTable("sucursales");

                entity.HasIndex(e => e.CodMunicipio, "FK_COD_MUN_idx");

                entity.Property(e => e.CodSucursal).HasColumnName("cod_sucursal");

                entity.Property(e => e.Activa)
                    .HasMaxLength(1)
                    .HasColumnName("activa")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.CodMunicipio).HasColumnName("cod_municipio");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodMunicipioNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.CodMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_MUN");
            });

            modelBuilder.Entity<SucursalesUsuario>(entity =>
            {
                entity.HasKey(e => new { e.CodSucursal, e.CodUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("sucursales_usuario");

                entity.HasIndex(e => e.CodUsuario, "FK_COD_USUARIO_SUCURSAL_idx");

                entity.Property(e => e.CodSucursal).HasColumnName("cod_sucursal");

                entity.Property(e => e.CodUsuario).HasColumnName("cod_usuario");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodSucursalNavigation)
                    .WithMany(p => p.SucursalesUsuarios)
                    .HasForeignKey(d => d.CodSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_SUCURSAL_SUCURSALES");

                entity.HasOne(d => d.CodUsuarioNavigation)
                    .WithMany(p => p.SucursalesUsuarios)
                    .HasForeignKey(d => d.CodUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COD_USUARIO_SUCURSAL");
            });

            modelBuilder.Entity<TipoBoleto>(entity =>
            {
                entity.HasKey(e => e.CodTipoBoleto)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_boleto");

                entity.Property(e => e.CodTipoBoleto).HasColumnName("cod_tipo_boleto");

                entity.Property(e => e.Activo)
                    .HasMaxLength(1)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<TiposAsiento>(entity =>
            {
                entity.HasKey(e => e.CodTipoAsiento)
                    .HasName("PRIMARY");

                entity.ToTable("tipos_asientos");

                entity.Property(e => e.CodTipoAsiento).HasColumnName("cod_tipo_asiento");

                entity.Property(e => e.Activo)
                    .HasMaxLength(1)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<TiposSala>(entity =>
            {
                entity.HasKey(e => e.CodTipoSala)
                    .HasName("PRIMARY");

                entity.ToTable("tipos_salas");

                entity.Property(e => e.CodTipoSala).HasColumnName("cod_tipo_sala");

                entity.Property(e => e.Activo)
                    .HasMaxLength(1)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CodUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.CodUsuario).HasColumnName("cod_usuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.FechaAct).HasColumnName("fecha_act");

                entity.Property(e => e.FechaIng).HasColumnName("fecha_ing");

                entity.Property(e => e.Nit)
                    .HasMaxLength(30)
                    .HasColumnName("nit");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombres");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("password");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(45)
                    .HasColumnName("telefono");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");
            });


            modelBuilder.Entity<ImagenPelicula>(entity =>
            {
                entity.HasKey(e => e.cod_imagen)
                    .HasName("PRIMARY");

                entity.ToTable("imagenespeliculas");

                entity.Property(e => e.cod_imagen).HasColumnName("cod_imagen");

                
                entity.Property(e => e.cod_pelicula)
                    .HasMaxLength(500)
                    .HasColumnName("cod_pelicula");

                entity.Property(e => e.imagen)
                .HasColumnType("LONGBLOB")
                .HasColumnName("imagen");

                entity.Property(e => e.tipo_imagen).HasColumnName("tipo_imagen");

                entity.HasOne(d => d.codTipoImagenNavigator)
                   .WithMany(p => p.Imagenes)
                   .HasForeignKey(d => d.tipo_imagen)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_TIPO_IMG_IMAGENES");

                entity.HasOne(d => d.codPeliculaNavigation)
                   .WithMany(p => p.Imagenes)
                   .HasForeignKey(d => d.cod_pelicula)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_COD_PELICULA_IMAGE");


            });
            modelBuilder.Entity<TipoImagen>(entity =>
            {
                entity.HasKey(e => e.cod_tipo_imagen)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_imagen");

                entity.Property(e => e.cod_tipo_imagen).HasColumnName("cod_tipo_imagen");


                entity.Property(e => e.nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.fecha_ing).HasColumnName("fecha_ing");


                entity.Property(e => e.usuario_ing)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

               
            });

            modelBuilder.Entity<Promociones>(entity =>
            {
                entity.HasKey(e => e.id_promocion)
                    .HasName("PRIMARY");

                entity.ToTable("promociones");

                entity.Property(e => e.id_promocion).HasColumnName("id_promocion");


                entity.Property(e => e.nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.estado)
                    .HasMaxLength(1)
                    .HasColumnName("estado");

                entity.Property(e => e.fecha_ing).HasColumnName("fecha_ing");


                entity.Property(e => e.usuario_ing)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_ing");

                entity.Property(e => e.usuario_act)
                    .HasMaxLength(45)
                    .HasColumnName("usuario_act");


            });




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
