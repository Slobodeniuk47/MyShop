package org.example.dto.accountDTO;

import java.time.LocalDateTime;

public class UserItemDTO {
    protected Integer id;
    protected String name;
    protected boolean IsDelete;
    protected LocalDateTime dateCreated;
    protected LocalDateTime dateUpdated;
    private String email;
    private String phoneNumber;
    private String passwordHash;
}
