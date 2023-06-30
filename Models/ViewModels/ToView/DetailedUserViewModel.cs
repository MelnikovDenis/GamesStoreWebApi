namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class DetailedUserViewModel(
    string Email,
    string Username,
    bool EmailConfirmed
    );