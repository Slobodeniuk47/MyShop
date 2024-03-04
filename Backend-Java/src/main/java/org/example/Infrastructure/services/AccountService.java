package org.example.Infrastructure.services;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.Infrastructure.dto.accountDTO.*;
import org.example.DAL.entities.account.UserEntity;
import org.example.DAL.entities.account.UserRoleEntity;
import org.example.Infrastructure.google.GoogleAuthService;
import org.example.Infrastructure.interfaces.IServices.IAccountService;
import org.example.Infrastructure.interfaces.IMappers.IUserMapper;
import org.example.DAL.repositories.IRoleRepository;
import org.example.DAL.repositories.IUserRepository;
import org.example.DAL.repositories.IUserRoleRepository;
import org.example.Infrastructure.storage.FileSaveFormat;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.multipart.MultipartFile;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class AccountService implements IAccountService {

    private final IUserRepository _userRepository;
    private final IUserRoleRepository _userRoleRepository;
    private final IRoleRepository _roleRepository;
    private final JwtTokenService _jwtTokenService;
    private final IUserMapper _userMapper;
    private final PasswordEncoder _passwordEncoder;
    private final IStorageService _storageService;
    private final GoogleAuthService _googleAuthService;

    @Override
    public String googleExternalLogin(@Valid @ModelAttribute ExternalLoginDTO model) {
        try {
            var googleUserInfo = _googleAuthService.verify(model.getToken());
            var checkUser = _userRepository.findByEmail(googleUserInfo.getEmail());
            if(!checkUser.isPresent()) {
                UserEntity newUser = UserEntity.builder()
                        .image("image")
                        .email(googleUserInfo.getEmail())
                        .firstname("firstname")
                        .lastname("lastname")
                        .passwordHash("password")
                        .phoneNumber("phonenumber")
                        .isGoogle(true)
                        .build();
                _userRepository.save(newUser);
                //Set role for user
                var roleName = "User";
                var permission = new UserRoleEntity().builder()
                        .role(_roleRepository.findByName(roleName))
                        .user(newUser)
                        .build();
                _userRoleRepository.save(permission);
                return _jwtTokenService.generateAccessToken(newUser);
            }
            var jwtToken = _jwtTokenService.generateAccessToken(checkUser.get());
            return jwtToken;
        }
        catch(Exception ex) {
           return "(HttpStatus.UNAUTHORIZED)";
        }
    }
    @Override
    public String login(LoginDTO request) {
        var user = _userRepository.findByEmail(request.getEmail())
                .orElseThrow();

        var isValid = _passwordEncoder.matches(request.getPassword(), user.getPasswordHash());
        if(!isValid) {
            throw new UsernameNotFoundException("User not found");
        }

        var jwtToken = _jwtTokenService.generateAccessToken(user);
        return jwtToken;
    }
    @Override
    public List<UserItemDTO> getAll() {
        var categories = _userRepository.findAll();
        return _userRepository.saveAll(categories)
                .stream()
                .map(_userMapper::UserItemDTOByUserEntity)
                .collect(Collectors.toList());
    }
    @Override
    public UserItemDTO getById(int id) {
        var userOptional = _userRepository.findById(id);

        if(userOptional.isPresent())
        {
            var result = _userMapper.UserItemDTOByUserEntity(userOptional.get());
            return result;
        }
        return null;
    }
    @Override
    public String register(UserCreateDTO model) {
        var user = _userRepository.findByEmail(model.getEmail());
        if(user.isPresent())
            return "The user is already registered!";
        //UserEntity newUser = _userMapper.itemDtoToUser(model);
        String fileName = _storageService.saveByFormat(model.getImage(), FileSaveFormat.PNG);
        UserEntity newUser = UserEntity.builder()
                .image(fileName)
                .email(model.getEmail())
                .firstname(model.getFirstname())
                .lastname(model.getLastname())
                .passwordHash(_passwordEncoder.encode(model.getPassword()))
                .phoneNumber(model.getPhoneNumber())

        .build();
        _userRepository.save(newUser);
        //Set role for user
        var roleName = model.getRole().length() == 0 ? "User" : model.getRole();
        var permission = new UserRoleEntity().builder()
                .role(_roleRepository.findByName(roleName))
                .user(newUser)
                .build();
        _userRoleRepository.save(permission);
        var jwtToken = _jwtTokenService.generateAccessToken(newUser);
        return jwtToken;
    }
    @Override
    public ResponseService edit(UserEditDTO model) {
        var user = _userRepository.findById(model.getId()).get();
        var userByEmail = _userRepository.findByEmail(model.getEmail());

        if(userByEmail.isPresent() && userByEmail.get().getId() != model.getId())
            return new ResponseService("Email already exists!", HttpStatus.NOT_ACCEPTABLE);
        UserEntity newUser = user;

        if (!Objects.isNull(model.getImageUpload())) {
            String fileName = model.getImageUpload().toString();
            _storageService.removeFile(user.getImage());
            fileName = _storageService.saveByFormat(model.getImageUpload(), FileSaveFormat.PNG);
            newUser.setImage(fileName);
        }
        newUser.setEmail(model.getEmail());
        newUser.setFirstname(model.getFirstname());
        newUser.setLastname(model.getLastname());
        newUser.setPhoneNumber(model.getPhoneNumber());
        newUser.setDateUpdated(LocalDateTime.now());

        _userRepository.save(newUser);
        if(model.getRole().length() != 0) {
            //Delete old role
            var permissions = _userRoleRepository.findByUser(user);
            _userRoleRepository.deleteAll(permissions);
            //Create new role for user
            var permission = new UserRoleEntity().builder()
                    .role(_roleRepository.findByName(model.getRole()))
                    .user(newUser)
                    .build();
            _userRoleRepository.save(permission);
            List<UserRoleEntity> s = new ArrayList<>();
                    s.add(permission);
            newUser.setUserRoles(s);
        }
        return new ResponseService(newUser, HttpStatus.CREATED);
    }
    @Override
    public void deleteById(int id) {
        UserEntity user = _userRepository.getById(id);
        _storageService.removeFile(user.getImage());
        var a = _userRoleRepository.findByUser(user);
        _userRoleRepository.deleteAll(a);
        _userRepository.deleteById(user.getId());
    }
}
