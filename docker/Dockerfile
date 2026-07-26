FROM golang:1.18 as builder
WORKDIR /app
COPY main.go .
RUN CGO_ENABLED=0 GOOS=linux go build -a -o hello-go main.go
FROM alpine:3.15
WORKDIR /app
COPY --from=builder /app/hello-go .
CMD ["/app/hello-go"]
