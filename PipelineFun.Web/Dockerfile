FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50914
EXPOSE 44322

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PipelineFun.Web/PipelineFun.Web.csproj", "PipelineFun.Web/"]
RUN dotnet restore "PipelineFun.Web/PipelineFun.Web.csproj"
COPY . .
WORKDIR "/src/PipelineFun.Web"
RUN dotnet build "PipelineFun.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PipelineFun.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PipelineFun.Web.dll"]