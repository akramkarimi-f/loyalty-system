using LoyaltySystem.Application;
using LoyaltySystem.Application.Users;
using LoyaltySystem.Domain;
using Moq;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICachingRepository> _cachingRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _cachingRepositoryMock = new Mock<ICachingRepository>();
        _userService = new UserService(_userRepositoryMock.Object, _cachingRepositoryMock.Object);
    }

    [Fact]
    public async Task EarnPointsAsync_UserExists_ShouldAddPointsAndUpdateRepositories()
    {
        // Arrange
        var userId = 2;
        var externalId = "external-id";
        var name = "Test User";
        var pointsToAdd = 10;
        var user = new User { Id = userId, ExternalId = externalId, Name = name, Points = 20 };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        await _userService.EarnPointsAsync(userId, pointsToAdd);

        // Assert
        Assert.Equal(30, user.Points);
        _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        _cachingRepositoryMock.Verify(cache => cache.SetUserPointsAsync(userId, 30), Times.Once);
    }

    [Fact]
    public async Task EarnPointsAsync_UserDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var userId = 1;
        var pointsToAdd = 10;

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((User?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _userService.EarnPointsAsync(userId, pointsToAdd));
        Assert.Equal("User not found", exception.Message);

        _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        _cachingRepositoryMock.Verify(cache => cache.SetUserPointsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
}
