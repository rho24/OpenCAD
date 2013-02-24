#version 400
precision highp float; 

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

layout (location = 0) in vec3 vert_position; 
layout (location = 1) in vec3 vert_normal; 
layout (location = 2) in vec4 vert_colour; 
out vec4 col;

void main(void) 
{ 
    gl_Position = Projection * View * Model * vec4(vert_position, 1); 
    col = vert_colour;
}