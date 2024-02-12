package org.example.services;

import lombok.AllArgsConstructor;
import lombok.RequiredArgsConstructor;
import org.example.configuration.security.JwtService;
import org.example.dto.accountDTO.AuthResponseDTO;
import org.example.dto.accountDTO.LoginDTO;
import org.example.dto.accountDTO.RegisterDTO;
import org.example.dto.accountDTO.UserItemDTO;
import org.example.dto.categoryDTO.CategoryItemDTO;
import org.example.entities.UserEntity;
import org.example.mappers.ApplicationMapper;
import org.example.repositories.UserRepository;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class AccountService {

    private final UserRepository _userRepository;
    private final PasswordEncoder _passwordEncoder;
    private final JwtService _jwtService;
    private final ApplicationMapper _appMapper;

    public AuthResponseDTO login(LoginDTO request) {
        var user = _userRepository.findByEmail(request.getEmail())
                .orElseThrow();

        //var password = passwordEncoder.
        var isValid = _passwordEncoder.matches(request.getPassword(), user.getPasswordHash());
        if(!isValid) {
            throw new UsernameNotFoundException("User not found");
        }

        var jwtToken = _jwtService.generateAccessToken(user);
        return AuthResponseDTO.builder()
                .token(jwtToken)
                .build();
    }

    public List<UserItemDTO> getAll() {
        var categories = _userRepository.findAll();
        return _userRepository.saveAll(categories)
                .stream()
                .map(_appMapper::UserItemDTOByUserEntity)
                .collect(Collectors.toList());
    }

    public UserItemDTO getById(int id) {
        var catOptional = _userRepository.findById(id);

        if(catOptional.isPresent())
        {
            var result = _appMapper.UserItemDTOByUserEntity(catOptional.get());
            return result;
        }
        return null;
    }
    public ResponseService register(RegisterDTO model) {
        var user = _userRepository.findByEmail(model.getEmail());
        if(user.isPresent())
            throw new UsernameNotFoundException("The user is already registered!");
        UserEntity newUser = _appMapper.itemDtoToUser(model);
        newUser.setPasswordHash(_passwordEncoder.encode(model.getPassword()));
        _userRepository.save(newUser);
        return new ResponseService(newUser, HttpStatus.CREATED);
    }
}
