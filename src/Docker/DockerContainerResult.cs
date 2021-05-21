using Docker.DotNet.Models;

namespace Example.Docker
{
    public class DockerContainerResult
    {
        public ContainerListResponse ContainerListResponse { get; set; }
        public ContainerInspectResponse ContainerInspectResponse { get; set; }

        public DockerContainerResult(
            ContainerListResponse containerListResponse,
            ContainerInspectResponse containerInspectResponse)
        {
            ContainerListResponse = containerListResponse;
            ContainerInspectResponse = containerInspectResponse;
        }
    }
}
