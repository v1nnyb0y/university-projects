FROM node:latest
WORKDIR /usr/src/app

COPY package*.json ./
RUN npm install

COPY . .
EXPOSE 2093
ENTRYPOINT [ "npm", "run", "start" ]