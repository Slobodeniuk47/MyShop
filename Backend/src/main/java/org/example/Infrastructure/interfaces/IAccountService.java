package org.example.Infrastructure.interfaces;

import org.example.Infrastructure.dto.accountDTO.LoginDTO;
import org.example.Infrastructure.dto.accountDTO.UserCreateDTO;
import org.example.Infrastructure.dto.accountDTO.UserEditDTO;
import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.example.Infrastructure.services.ResponseService;

import java.util.List;

public interface IAccountService {
    String login(LoginDTO request);
    List<UserItemDTO> getAll();
    UserItemDTO getById(int id);
    ResponseService register(UserCreateDTO model);
    ResponseService edit(UserEditDTO model);
    void deleteById(int id);
}
