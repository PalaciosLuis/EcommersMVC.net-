using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyEcomersMVC.Models;
using ProyEcomersMVC.DAO;

namespace ProyEcomersMVC.Controllers
{
    public class VentasController : Controller
    {

        //DEFINIR LAS VARIABLES DE LOS DAO A UTILIZAR
        ArticulosDAO dao_arti =new ArticulosDAO();
        ClientesDAO dao_cli=new ClientesDAO();

        //Definir una variable que almacene los articulos del carrito de compra
        List<Carrito> lista_car=new List<Carrito>();



        // GET: Ventas
        public ActionResult Listar_Articulos(string nom="")
        {

            //Si no existe la variable de sesion entonces lo creamos
            if (Session["carrito"]==null)
            {
                Session["carrito"] = lista_car;
            }

            var listado = dao_arti.ListarArticulos(nom);

            return View(listado);
        }

        // GET: Ventas/Details/5
        public ActionResult SeleccionarArticulos(string id)
        {
             //todos los articulos
            var listado = dao_arti.ListarArticulos("");

            //obtener al articulo con la variable id
            Articulos buscado =listado.Find(a=>a.art_cod.Equals(id));


            return View(buscado);
        }




        //=================================================
        public ActionResult AgregarArticulo(string art_cod="")
        {


            //ARTICULO QUE SERA AGREGADO AL CARRITO DE COMPRAS
            //todos los articulos

            //obtener al articulo con la variable id
            Articulos buscado = dao_arti.ListarArticulos("").Find(a => a.art_cod.Equals(art_cod));

            //RECUPERAR CARRITO
            lista_car = Session["carrito"] as List<Carrito>;

            //BUSCAR EL CODIGO DEL NUEVO ARTICULO NO SE ENCUENTRE EN lista_car
            Carrito encontrado = lista_car.Find(a => a.codigo.Equals(art_cod));


         
            
            if (encontrado==null)
            {

                Carrito car = new Carrito()
                {


                    codigo = buscado.art_cod,
                    nombre = buscado.art_nom,
                    precio = buscado.art_pre,
                    cantidad = 1
                };

                //Si no se encuentra el articulo en el carrito entonces lo agregamos al carrito
                lista_car.Add(car);
                ViewBag.Mensaje = $"el articulo: {car.nombre} fue agregado al carrito";

            }
            else
            {
                encontrado.cantidad++;
                ViewBag.Mensaje = $"el articulo: {encontrado.nombre} Incremento su cantidad {encontrado.cantidad}";
            }

            //Actualizar carrito de compra

            Session["carrito"] = lista_car;
            ViewBag.codigo=art_cod; //buscado.art_code //car.codigo

            return View();
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
