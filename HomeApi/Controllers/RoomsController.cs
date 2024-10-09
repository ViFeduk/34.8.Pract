using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Rewrite(
            [FromRoute] Guid id,
            [FromBody] RewriteRoomRequest request)
        {
            var room = await _repository.GetRoomByName(request.Name);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.Name} не подключена. Сначала подключите комнату!");

           await _repository.RewriteRoom(room,request);


            return StatusCode(200, $"Комната перезаписана ! Имя - {room.Name}, Площадь - {room.Area}, Наличие газа - {room.GasConnected}");
        }
    }
}