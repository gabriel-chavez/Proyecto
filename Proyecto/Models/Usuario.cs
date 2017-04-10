namespace Proyecto.Models
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Web;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Experiencia = new HashSet<Experiencia>();
            Habilidad = new HashSet<Habilidad>();
            Testimonio = new HashSet<Testimonio>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Column(TypeName = "text")]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Ciudad { get; set; }

        public int? Pais_id { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string YouTube { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Experiencia> Experiencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habilidad> Habilidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Testimonio> Testimonio { get; set; }
        public ResponseModel Acceder(string email, string password)
        {
            var rm = new ResponseModel();
            try
            {
                password = HashHelper.MD5(password);
                using (var ctx = new ProyectoContext())
                {
                    var usuario = ctx.Usuario.Where(x => x.Email == email)
                                            .Where(x => x.Password == password)
                                            .SingleOrDefault();
                    if(usuario!=null)
                    {
                        SessionHelper.AddUserToSession(usuario.id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Correo o contraseña incorrectos");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return rm;
        }
        public Usuario Obtener(int id)
        {
            var usuario = new Usuario();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    usuario = ctx.Usuario.Where(x => x.id == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return usuario;
        }
        public ResponseModel Guardar(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    var eUsuario = ctx.Entry(this);
                    
                    eUsuario.State = EntityState.Modified;
                    //para la foto
                    if(Foto!=null)
                    {
                        //nombre de archivo
                        string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);
                        Foto.SaveAs(HttpContext.Current.Server.MapPath("~/uploads/" + archivo));
                        this.Foto = archivo;
                    }
                    else
                    {
                        //si no tiene foto cargada ignoramos la foto
                        eUsuario.Property(x => x.Foto).IsModified = false;
                    }
                    //para ignorar cambios en el password
                    if (this.Password == null) eUsuario.Property(x => x.Password).IsModified = false;

                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch(DbEntityValidationException e)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            return rm;
        }
    }
}
