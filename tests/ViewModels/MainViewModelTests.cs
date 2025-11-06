using MSISDNWebClient.ViewModels;
using Xunit;

namespace MSISDNWebClient.Tests.ViewModels;

public class MainViewModelTests
{
    private readonly MainViewModel _viewModel = new();

    [Fact]
    public void Constructor_InitializesState()
    {
        Assert.NotNull(_viewModel);
        Assert.False(string.IsNullOrWhiteSpace(_viewModel.Title));
    }
}