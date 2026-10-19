# Sistema de veterinaria — Práctica 3

Aplicación web ASP.NET Core MVC para administrar pacientes veterinarios. Utiliza las clases de mascotas y una lista enlazada propia; no requiere base de datos.

## Cómo ejecutar

1. Abre una terminal en esta carpeta.
2. Ejecuta `dotnet run --project .\Practica3_V2.1_IPC2.csproj`.
3. Abre `http://localhost:5080` en el navegador. En VS Code, también puedes pulsar **F5** y seleccionar `Practica3_V2.1_IPC2`.

## Qué puedes hacer

- Registrar perros, gatos, aves y tortugas.
- Consultar, editar y eliminar fichas.
- Cambiar el estado de salud entre sana y enferma.
- Calcular dosis seleccionando la especie del menú (con el porcentaje de ajuste), el peso y los mg/kg.

Los códigos de mascota se muestran en el listado. Los datos se mantienen en memoria mientras la aplicación está en ejecución y se reinician al detenerla.

El cálculo de dosis es solo para la práctica y no sustituye la indicación de un profesional veterinario.
