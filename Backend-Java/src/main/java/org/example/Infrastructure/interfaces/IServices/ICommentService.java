package org.example.Infrastructure.interfaces.IServices;

import org.example.Infrastructure.dto.commentDTO.CommentCreateDTO;
import org.example.Infrastructure.dto.commentDTO.CommentEditDTO;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;

import java.util.List;

public interface ICommentService {
    CommentItemDTO create(CommentCreateDTO model);
    List<CommentItemDTO> getAll();
    CommentItemDTO edit(CommentEditDTO model);
    void deleteById(int id);
    CommentItemDTO getById(int id);
    List<CommentItemDTO> getCommentsByProductIdAsync(int id);
}
