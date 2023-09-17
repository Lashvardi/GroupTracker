﻿using GroupTracker.DTOs.Groups;
using GroupTracker.Services.Abstraction.Group;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {

        private readonly ILecturerGroupCoordinator _lecturerGroupCoordinator;

        public GroupController(ILecturerGroupCoordinator lecturerGroupCoordinator)
        {
            _lecturerGroupCoordinator = lecturerGroupCoordinator;
        }


        [HttpPost("create-and-assign")]
        public async Task<IActionResult> CreateAndAssignGroup(int lecturerId, [FromBody] CompleteGroupInput completeGroupInput)
        {
            try
            {
                var newGroup = await _lecturerGroupCoordinator.CreateAndAssignGroup(lecturerId, completeGroupInput);
                return Created($"Group created with ID: {newGroup.Id}", newGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}