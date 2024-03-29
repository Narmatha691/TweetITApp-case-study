﻿using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;
using System.Data;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public PostController(IPostService postService,IUserService userService, IMapper mapper, ILog logger)
        {
            this.postService = postService;
            this.userService= userService;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost, Route("AddPost")]
        [Authorize(Roles = "User")]
        //
        public IActionResult AddPost(PostWithoutIdDTO postdto)
        {
            Post post=_mapper.Map<Post>(postdto);
            post.User = userService.GetUserById(postdto.UserId);
            post.DateandTime = DateTime.Now;
            post.ValidatedorBlocked = null;
            post.ActionDoneById = null;
            post.ActionDOneUser = null;
            postService.AddPost(post);
            PostDTO postnew=_mapper.Map<PostDTO>(post);
            return StatusCode(200, postnew);
        }
        [HttpGet,Route("GetAllPosts")]
        [AllowAnonymous]
        //
        public IActionResult GetAllPost() 
        {
            try
            {

                List<PostDTO> posts = postService.GetAllPost();
                return StatusCode(200, posts);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("GetPostById/{postId}")]
        [AllowAnonymous]
        //
        public IActionResult GetPostById(int postId)
        {
            try
            {

                PostDTO post = postService.GetPostById(postId);               
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("SearchPostByTitle/{searchTerm}")]
        [AllowAnonymous]
        //
        public IActionResult SearchPostByTitle(string searchTerm)
        {
            try
            {

                List<PostDTO> post = postService.SearchPostsByTitle(searchTerm);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("SearchPostByUser/{searchName}")]
        [AllowAnonymous]
        //
        public IActionResult SearchPostByUser(string searchName)
        {
            try
            {

                List<PostDTO> post = postService.SearchPostsByUser(searchName);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("ListPostByUserId/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult ListPostByUserId(string userId)
        {
            try
            {

                List<PostDTO> post = postService.GetPostsByUserId(userId);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("GetPostsOfFollowing/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult GetPostsOfFollowing(string userId)
        {
            try
            {

                List<PostDTO> post = postService.GetPostsOfFollowing(userId);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }


        [HttpGet, Route("SearchPostsOfFollowing/{userId}/{searchterm}")]
        [AllowAnonymous]
        //
        public IActionResult SearchPostsOfFollowing(string userId,string searchterm)
        {
            try
            {

                List<PostDTO> post = postService.SearchPostsOfFollowing(userId,searchterm);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("SearchPostsByTitleAndUserId/{userId}/{searchTerm}")]
        [AllowAnonymous]
        //
        public IActionResult SearchPostsByTitleAndUserId(string userId, string searchTerm)
        {
            try
            {
                List<PostDTO> post = postService.SearchPostsByTitleAndUserId(userId,searchTerm);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }


        [HttpPut,Route("EditPost")]
        [Authorize(Roles = "User")]
        //
        public IActionResult EditPost(PostDTO postdto)
        {
            try
            {
                var result = postService.EditPost(postdto);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpDelete,Route("DeletePost/{PostId}")]
        [Authorize(Roles = "User")]
        //
        public IActionResult DeletePost(int PostId) 
        {
            try
            {
                var result = postService.DeletePost(PostId);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {

                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

    }
}
