using AIMathProject.Application.Command.Register;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AIMathProject.Application.Seeding
{
    public class UserSeeder
    {
        private readonly IServiceProvider _serviceProvider;

        public UserSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedUsersFromJson(string jsonFilePath, string role)
        {
            // Đọc file JSON
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);
            var userList = JsonConvert.DeserializeObject<List<RegisterRequest>>(jsonData);

            // Tạo scope để sử dụng MediatR
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Duyệt qua từng người dùng trong file JSON
                foreach (var userData in userList)
                {
                    try
                    {
                        // Tạo RegisterRequest từ dữ liệu JSON
                        var registerRequest = new RegisterRequest
                        {
                            UserName = userData.UserName,
                            Email = userData.Email,
                            Gender = userData.Gender,
                            Dob = userData.Dob,
                            Password = userData.Password,
                            Avatar = userData.Avatar,
                            Status = true
                        };

                        // Tạo RegisterCommand
                        var command = new RegisterCommand(registerRequest, role);

                        // Gọi handler thông qua MediatR để chèn người dùng
                        await mediator.Send(command);

                        Console.WriteLine($"Đã chèn người dùng {userData.Email} với vai trò User.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi chèn người dùng {userData.Email}: {ex.Message}");
                    }
                }
            }
        }
    }
}