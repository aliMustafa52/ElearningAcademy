global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;


global using ElearningAcademy.api.Abstractions;
global using ElearningAcademy.api.Authentication;
global using ElearningAcademy.api.Contracts.Authentication;
global using ElearningAcademy.api.Data;
global using ElearningAcademy.api.Entities;
global using ElearningAcademy.api.Entities.Enums;
global using ElearningAcademy.api.Errors;
global using ElearningAcademy.api.Repositories;
global using ElearningAcademy.api.Contracts.Curriculums;
global using ElearningAcademy.api.Services.CurriculumsService;
global using ElearningAcademy.api.Services.Lessons;
global using ElearningAcademy.api.Services.Topics;
global using ElearningAcademy.api.Services.FileStorageService;

global using FluentValidation;
global using Mapster;
global using MapsterMapper;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;

