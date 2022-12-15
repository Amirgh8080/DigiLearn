using BlogModule.Services;
using Common.Application;
using Moq;

namespace Blog.unit.Test.Services;

//[TestClass]
public class BlogServiceTest
{
    //private readonly IBlogService _blogService;
    //private readonly Mock<IBlogService> _blogServiceMock;

    //public BlogServiceTest(Mock<IBlogService> blogServiceMock)
    //{
    //    _blogServiceMock = blogServiceMock;
    //}

    [Fact]
    public void Should_Delete_Post_When_Given_Correct_Id()
    {
        var id = Guid.NewGuid();
        
        var mockRepo = new Mock<IBlogService>();

        mockRepo.Setup(r=>r.DeletePost(id)).ReturnsAsync(OperationResult.Success);
        //act 
        var result = mockRepo.Object;
        var res = result.DeletePost(id);

        //Assert
        //var ok = res.Should().Be(OperationResult.Success());
        var ok = Assert.IsType<OperationResult>(res);
            
    }
}
