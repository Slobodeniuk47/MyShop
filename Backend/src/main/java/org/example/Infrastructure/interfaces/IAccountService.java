package org.example.Infrastructure.interfaces;

import org.example.Infrastructure.dto.accountDTO.*;
import org.example.Infrastructure.services.ResponseService;

import java.util.List;

public interface IAccountService {
    String login(LoginDTO request);
    List<UserItemDTO> getAll();
    UserItemDTO getById(int id);
    String register(UserCreateDTO model);
    String googleExternalLogin(ExternalLoginDTO model);
    ResponseService edit(UserEditDTO model);
    void deleteById(int id);
}
