#!/bin/bash
IMAGE="dotnet:1.0"
# stop previous golang container and remove image
docker stop dotnetcore
docker rmi $IMAGE
# compile image
docker build -t $IMAGE ./server/WebApiSenseWord
docker run -d --name=dotnetcore --rm -p 8084:8084 $IMAGE
