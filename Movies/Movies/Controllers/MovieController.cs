﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Formatting;
using System.Reflection.PortableExecutable;


namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public MovieController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //add
        [HttpPost]
        [Route("InsertMovies")]
        
        public IActionResult InsertData(Movie mvs)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("connectionDB").ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertDataMovies", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = mvs.title;
                        cmd.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = mvs.description;
                        cmd.Parameters.Add("@rating", SqlDbType.VarChar).Value = mvs.rating;
                        cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = mvs.image;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return Ok("Insert data successfully");
            }
            catch (Exception e)
            {

                Response.StatusCode = BadRequest().StatusCode;
                return new JsonResult(new {  title = "Error",message = e.Message.ToString() });

            }
        }

        [HttpGet]
        [Route("Movies")]

        public IActionResult ShowData()
        {
            SqlDataReader reader = null;
            DataTable dt = new DataTable();
            List<Movie> movieData = new List<Movie>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("connectionDB").ToString());
            using (SqlCommand cmd = new SqlCommand("select * from mstmovie", con))
            {
                //cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "select * from mstmovie";
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Movie mv = new Movie();
                    mv.id = Convert.ToInt32(reader.GetValue(0));
                    mv.title = reader.GetValue(1).ToString();
                    mv.description = reader.GetValue(2).ToString();
                    mv.rating = float.Parse(reader.GetValue(3).ToString());
                    mv.image = reader.GetValue(4).ToString();
                    mv.created_at = DateTime.Parse(reader.GetValue(5).ToString());

                    var updateat = reader.GetValue(6).ToString();
                    if (updateat.ToString().Trim() != "")
                        mv.updated_at = DateTime.Parse(updateat);
                    movieData.Add(mv);
                }
            }
                
            if(movieData.Count > 0)
                return new JsonResult(movieData);
            else
                return new NotFoundResult();
        }

        //[HttpGet("Movies/{id}")]
        [HttpGet]
        [Route("Movies/{id}")]

        public IActionResult ShowDataParam([FromRoute(Name = "id")] int id)
        {
            SqlDataReader reader = null;
            DataTable dt = new DataTable();
            List<Movie> movieData = new List<Movie>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("connectionDB").ToString());
            using (SqlCommand cmd = new SqlCommand("select * from mstmovie where id="+id.ToString().Trim(), con))
            {
                //cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "select * from mstmovie";
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Movie mv = new Movie();
                    mv.id = Convert.ToInt32(reader.GetValue(0));
                    mv.title = reader.GetValue(1).ToString();
                    mv.description = reader.GetValue(2).ToString();
                    mv.rating = float.Parse(reader.GetValue(3).ToString());
                    mv.image = reader.GetValue(4).ToString();
                    mv.created_at = DateTime.Parse(reader.GetValue(5).ToString());

                    var updateat = reader.GetValue(6).ToString();
                    if (updateat.ToString().Trim() != "")
                        mv.updated_at = DateTime.Parse(updateat);
                    movieData.Add(mv);
                }
            }

            if (movieData.Count > 0)
                return new JsonResult(movieData);
            else
                return new NotFoundResult();
        }



        [HttpPut]
        [Route("UpdateMovie/{id}")]
        public IActionResult UpdateMovie(Movie mv)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("connectionDB").ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateDataMovies", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = mv.id;
                        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = mv.title;
                        cmd.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = mv.description;
                        cmd.Parameters.Add("@rating", SqlDbType.VarChar).Value = mv.rating;
                        cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = mv.image;
                        cmd.Parameters.Add("@updated_at", SqlDbType.VarChar).Value = DateTime.Now;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return Ok("Update data successfully");
            }
            catch (Exception e)
            {

                Response.StatusCode = BadRequest().StatusCode;
                return new JsonResult(new { title = "Error", message = e.Message.ToString() });

            }
        }

        [HttpDelete]
        [Route("DeleteMovie/{id}")]
        public IActionResult DeleteMovie([FromRoute(Name = "id")] int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("connectionDB").ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteDataMovies", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return Ok("Delete data successfully");
            }
            catch (Exception e)
            {

                Response.StatusCode = BadRequest().StatusCode;
                return new JsonResult(new { title = "Error", message = e.Message.ToString() });

            }
        }









    }
}
