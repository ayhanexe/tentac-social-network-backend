using AutoMapper;
using DomainModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TentacSocialPlatformApi.Controllers.Abstraction
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TEntityPrimaryKey, TEntityDto, TRepository> : ControllerBase
        where TEntity : class
        where TEntityDto : class
        where TRepository : IRepository<TEntity, TEntityPrimaryKey>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        public BaseController(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(TEntityPrimaryKey id)
        {
            var entity = await _repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add(TEntityDto entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                var test = _mapper.Map<TEntity>(entity);
                await _repository.Add(_mapper.Map<TEntity>(entity));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TEntityPrimaryKey id, TEntity entity)
        {
            TEntity _entity = await _repository.Get(id);

            if (_entity == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.Update(entity);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TEntityPrimaryKey id)
        {
            var entity = await _repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
